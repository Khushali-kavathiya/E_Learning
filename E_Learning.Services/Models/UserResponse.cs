namespace E_Learning.Services.Models;

/// <summary>
/// Represents a user returned by the GetAllUsers API.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Unique identifier of the user.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Full name of the user.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Roles assigned to the user.
    /// </summary>
    public IList<string> Roles { get; set; }
}