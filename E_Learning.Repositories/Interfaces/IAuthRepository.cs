using E_Learning.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Learning.Repositories.Interface;

public interface IAuthRepository
{
    /// <summary>
    /// Creates a new user with the specified password.
    /// </summary>
    Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

    /// <summary>
    /// Adds a user to a specified role.
    /// </summary>
    Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    Task<ApplicationUser> GetUserByEmailAsync(string email);

    /// <summary>
    /// Checks if the provided password matches the user's password.
    /// </summary>
    Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);
}