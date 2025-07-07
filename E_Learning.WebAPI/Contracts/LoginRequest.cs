namespace E_Learning.WebAPI.Contracts;

public class LoginRequest
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