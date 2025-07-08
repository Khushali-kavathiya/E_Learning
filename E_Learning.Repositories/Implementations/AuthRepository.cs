using E_Learning.Domain.Entities;
using E_Learning.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Implementations;

/// <summary>
/// Repository for handling authentication-related operations.
/// </summary>
public class AuthRepository : IAuthRepository
{
    /// <summary>
    /// UserManager instance for managing user-related operations.
    /// </summary>
    /// <param name="userManager">UserManager instance injected via dependency injection.</param>
    /// <returns>Instance of AuthRepository.</returns>
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    /// <summary>
    /// Creates a new user with the specified password.
    /// </summary>
    /// <param name="user">The user to be created.</param>
    public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    /// <summary>
    /// Adds a user to a specified role.
    /// </summary>
    /// <param name="user">The user to be added to the role.</param>
    public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user to be retrieved.</param>
    /// <returns>The user with the specified email address, or null if not found.</returns
    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    /// <summary>
    /// Checks if the provided password matches the user's password.
    /// </summary>  
    /// <param name="applicationUser">The user whose password is to be checked.</param>
    /// <param name="password">The password to check against the user's password.</param>
    /// <returns>True if the password matches, otherwise false.</returns>
    public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
    {
        return await _userManager.CheckPasswordAsync(applicationUser, password);
    }

    /// <summary>
    /// Finds a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user to be retrieved.</param>
    /// <returns>The user with the specified ID, or null if not found.</returns>
    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
    }

}