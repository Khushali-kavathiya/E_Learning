namespace E_Learning.Services.Models;

/// <summary>
/// Represents the registration request data sent by the user.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Full name of the user registering.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Email address of the user. This will also be used as the username.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Plain text password entered by the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Optional role for the user. If not provided, a default role (e.g., "Student") may be assigned.
    /// </summary>
    public string? Role { get; set; }
}