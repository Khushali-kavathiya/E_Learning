using Microsoft.AspNetCore.Identity;

namespace E_Learning.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// FullName of the User.
    /// </summary>
    public string FullName { get; set; }

}