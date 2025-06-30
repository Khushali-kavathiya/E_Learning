using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using E_Learning.Services.Implementation;
using E_Learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Learning.Extensions.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Configure PostgreSQL database with Entity Framework Core
builder.Services.AddDbContext<E_LearningDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

// Register ASP.NET Identity services with custom ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<E_LearningDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAutoMapper(typeof(UserMappingProfile));    

// Add other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
