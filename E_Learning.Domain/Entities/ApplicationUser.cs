using Microsoft.AspNetCore.Identity;

namespace E_Learning.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}