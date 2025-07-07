using E_Learning.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Data;

/// <summary>
/// Database context for the E-Learning application.
/// Inherits from IdentityDbContext to manage user authentication and authorization.
/// </summary>
public class E_LearningDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="E_LearningDbContext"/> class with the specified options.
    /// This constructor is used to configure the database context with the provided options.
    /// </summary>
    /// <param name="options"></param>
    public E_LearningDbContext(DbContextOptions<E_LearningDbContext> options) : base(options)
    {
    }

}