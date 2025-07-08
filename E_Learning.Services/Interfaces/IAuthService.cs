using Microsoft.AspNetCore.Identity;
using E_Learning.Services.Models;

namespace E_Learning.Services.Interface;

public interface IAuthService
{
   /// <summary>
   /// Registers a new user with the provided model.
   /// Maps the model to an entity, creates the user, and adds them to a specified role.
   /// </summary>
   /// <param name="model">The model containing user registration details.</param>
   /// <returns>A task that represents the asynchronous operation, containing the result of the registration.</returns>
   Task<IdentityResult> RegisterAsync(RegisterUserModel model);

   /// <summary>
   /// Logs in a user with the provided credentials.
   /// Validates the user's email and password, and generates a JWT token if successful.
   /// </summary>
   /// <param name="model">The model containing user login details.</param>
   /// <returns>A task that represents the asynchronous operation, containing the JWT token if the login is successful; otherwise, throws an UnauthorizedAccessException.</returns>
   Task<string> LoginAsync(LoginRequestModel model);

   /// <summary>
   /// Retrieves the current user's profile based on their user ID.
   /// </summary>
   /// <param name="userId">The ID of the user whose profile is to be retrieved.</param>
   /// <returns>A task that represents the asynchronous operation, containing the user's profile.</returns>
   Task<UserProfileModel> GetCurrentUserProfileAsync(string userId);

}