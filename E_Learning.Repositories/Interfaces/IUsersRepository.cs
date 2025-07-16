using E_Learning.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Learning.Repositories.Interface;

/// <summary>
/// Interface for user-related repository operations such as creation, retrieval, update,
/// password management, role assignment, and deletion.
/// </summary>

public interface IUsersRepository
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

    /// <summary>
    /// Finds a user by their ID.
    /// </summary>
    Task<ApplicationUser> GetUserByIdAsync(string userId);

    /// <summary>
    /// Updates an existing user's properties in the database.
    /// </summary>
    Task<IdentityResult> UpdateUserAsync(ApplicationUser user);

    /// <summary>
    /// Removes the password for the specified user.
    /// </summary>
    Task<IdentityResult> RemovePasswordAsync(ApplicationUser user);

    /// <summary>
    /// Adds a new password for the specified user.
    /// </summary>

    Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);

    /// <summary>
    /// Retrieves all users in the system.
    /// </summary>
    Task<List<ApplicationUser>> GetAllUsersAsync();

    /// <summary>
    /// Gets the roles assigned to the specified user.
    /// </summary>
    Task<IList<string>> GetUserRolesAsync(ApplicationUser user);

    /// <summary>
    /// Deletes a user from the system.
    /// </summary>
    Task<IdentityResult> DeleteUserByIdAsync(ApplicationUser user);

}