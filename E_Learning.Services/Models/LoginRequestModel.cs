namespace E_Learning.Services.Models;

public class LoginRequestModel
{
    /// <summary>
    /// Gets or sets the email of the user attempting to log in.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user attempting to log in.
    /// </summary>
    public string Password { get; set; }
}
