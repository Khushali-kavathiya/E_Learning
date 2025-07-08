namespace E_Learning.WebAPI.Contracts;

/// <summary>
/// Represents the response containing user profile information.
/// </summary>
public class UserProfileResponse
{
    /// <summary>
    /// unique identifier of the user.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// full name of the user.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// username of the user.
    /// </summary>
    public string UserName { get; set; }
}