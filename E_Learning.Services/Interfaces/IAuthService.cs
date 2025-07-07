using Microsoft.AspNetCore.Identity;
using E_Learning.Services.Models;

namespace E_Learning.Services.Interface;

public interface IAuthService
{
   /// <summary>
   /// Registers a new user with the provided model.
   /// Maps the model to an entity, creates the user, and adds them to a specified role.
   /// </summary>
   Task<IdentityResult> RegisterAsync(RegisterUserModel model);

   /// <summary>
   /// Logs in a user with the provided credentials.
   /// Validates the user's email and password, and generates a JWT token if successful.
   /// </summary>
   Task<string> LoginAsync(LoginRequestModel model);

}