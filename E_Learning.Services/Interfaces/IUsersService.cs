using Microsoft.AspNetCore.Identity;
using E_Learning.Services.Models;

namespace E_Learning.Services.Interface;

/// <summary>
/// Interface defining user-related operations such as registration, authentication,
/// profile management, and user administration.
/// </summary>
public interface IUsersService
{
   /// <summary>
   /// Registers a new user with the provided model.
   /// Maps the model to an entity, creates the user, and adds them to a specified role.
   /// </summary>
   /// <param name="model">The model containing user registration details.</param>
   /// <returns>A task that represents the asynchronous operation, containing the result of the registration.</returns>
   Task<IdentityResult> RegisterAsync(UserModel model);

   /// <summary>
   /// Logs in a user with the provided credentials.
   /// Validates the user's email and password, and generates a JWT token if successful.
   /// </summary>
   /// <param name="model">The model containing user login details.</param>
   /// <returns>A task that represents the asynchronous operation, containing the JWT token if the login is successful; otherwise, throws an UnauthorizedAccessException.</returns>
   Task<string> LoginAsync(UserModel model);

   /// <summary>
   /// Retrieves a user by their unique identifier and maps to a <see cref="UserModel"/>.
   /// </summary>
   /// <param name="userId">The unique identifier of the user to retrieve.</param>
   /// <returns>A task representing the asynchronous operation, containing the <see cref="UserModel"/> if found; otherwise, null.</returns>
   Task<UserModel> GetUserByIdAsync(string userId);

   /// <summary>
    /// Applies a partial update to an existing user's data.
    /// </summary>
    /// <param name="userId">The ID of the user to patch.</param>
    /// <param name="updated">The updated model containing modified values.</param>
    /// <returns>
    /// A task representing the asynchronous operation, returning the result of the update.
    /// </returns>
   Task<IdentityResult> PatchUserAsync(string userId, UserModel updated);

    /// <summary>
    /// Retrieves a list of all registered users in the system.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation, containing a list of <see cref="UserModel"/>.
    /// </returns>
   Task<List<UserModel>> GetAllUsersAsync();

   /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>
    /// A task representing the asynchronous operation, returning the result of the delete operation.
    /// </returns>
   Task<IdentityResult> DeleteUserByIdAsync(string userId);

}