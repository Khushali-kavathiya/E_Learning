using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using E_Learning.WebAPI.Validators;
using E_Learning.WebAPI.Middlewares;
using E_Learning.Services.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using E_Learning.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Configure global API Versioning and validation
builder.Services.AddControllers(options =>
{
    options.Conventions.Insert(0, new GlobalRoutePrefixConvention("api/v{version:apiVersion}"));
})
.AddNewtonsoftJson(options =>
{
    // This is what fixes enum serialization when using Newtonsoft
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
}) // This is required for JsonPatchDocument
.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<UserContractValidator>();
});



builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Register Swagger/OpenAPI for API documentation and testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "E_Learning API", Version = "v1" });

    // Configure JWT authentication UI in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter your JWT token.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // Make JWT security a requirement for all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }
});
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configure PostgreSQL database with Entity Framework Core
builder.Services.AddDbContext<E_LearningDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

// Register ASP.NET Identity services with custom ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<E_LearningDbContext>()
    .AddDefaultTokenProviders();


// Register AutoMapper globally
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Automatically scan for profiles in the All assembly

// Configure JWT authentication
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings")
);

//This adds JwtSettings directly to DI (needed for primary constructor injection)
builder.Services.AddSingleton(
    builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()
);
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero // Remove delay of token expiration
    };
});
builder.Services.AddAuthorization();

// Register HttpContextAccessor for accessing HttpContext in services
builder.Services.AddHttpContextAccessor();


// Automatic registering services using Scrutor package for scanning assemblies
builder.Services.Scan(scan => scan
    .FromApplicationDependencies(a => a.GetName().Name.StartsWith("E_Learning"))
    .AddClasses(c => c.Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);

// Register HttpClient for making HTTP requests
builder.Services.AddHttpClient();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "E_Learning API V1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// Global exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();