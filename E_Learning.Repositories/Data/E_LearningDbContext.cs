using E_Learning.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Data;

public class E_LearningDbContext : IdentityDbContext<ApplicationUser>
{
    public E_LearningDbContext(DbContextOptions<E_LearningDbContext> options) : base(options)
    {
    }
    
}