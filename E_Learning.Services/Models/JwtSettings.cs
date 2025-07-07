namespace E_Learning.Services.Models;

public class JwtSettings
{
    /// <summary>
    /// Gets or sets the secret key used for signing JWT tokens.
    /// </summary>
    public string SecretKey { get; set; } = default!;

    /// <summary>
    /// Gets or sets the issuer of the JWT tokens.
    /// </summary>
    public string Issuer { get; set; } = default!;

    /// <summary>
    /// Gets or sets the audience for which the JWT tokens are intended.
    /// </summary>
    public string Audience { get; set; } = default!;

    /// <summary> 
    /// Gets or sets the expiration time in minutes for the JWT tokens.
    /// </summary>
    public int ExpirationMinutes { get; set; }
}
