using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Learning.Services.Interface;
using E_Learning.Services.Implementations;
using E_Learning.Repositories.Interface;
using E_Learning.Repositories.Implementations;
using FluentValidation.AspNetCore;
using E_Learning.WebAPI.Validators;
using E_Learning.WebAPI.Middlewares;
using E_Learning.WebAPI.Mapping;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using E_Learning.Services.Mapping;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure PostgreSQL database with Entity Framework Core
builder.Services.AddDbContext<E_LearningDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

// Register ASP.NET Identity services with custom ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<E_LearningDbContext>()
    .AddDefaultTokenProviders();


// Automapper configuration for mapping between contracts and models
builder.Services.AddAutoMapper(typeof(AuthContractModelMapperProfile));
builder.Services.AddAutoMapper(typeof(UserModelEntityMapper));

// Configure Identity options (validation, password settings, etc.)
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<RegisterUserContractValidator>();
    });

// Configure JWT authentication
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings")
);
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ClockSkew = TimeSpan.Zero // Set clock skew to zero for immediate expiration
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "E_Learning API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter your JWT token. Example: Bearer eyJhbGciOiJIUzI1NiIsInR...",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

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

// Register Services 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtTokensService, JwtTokensService>();

// Add other services
//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
