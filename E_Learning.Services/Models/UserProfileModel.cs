namespace E_Learning.Services.Models;

/// <summary>
/// Represents a user profile model containing basic user information.
/// </summary>
public class UserProfileModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.     
    /// /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>       
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.  
    /// </summary>
    public string UserName { get; set; }
}