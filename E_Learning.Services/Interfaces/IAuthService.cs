using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Registers a new user with the provided information and role.
    /// </summary>
    /// <param name="request">The registration details including email, password, full name, and role.</param>
    /// <returns>A string message indicating success or error.</returns>
    Task<string> RegisterAsync(RegisterRequest request);

    /// <summary>
    /// Logs in a user and returns a JWT token if credentials are valid.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <returns>Jwt Token.</returns>
    Task<string> LoginAsync(LoginRequest request);

    /// <summary>
    /// Retrives all registered users with their roles.
    /// </summary>
    /// <returns>List of users.</returns>
    Task<List<UserResponse>> GetAllUsersAsync();
}