namespace E_Learning.Extensions;

/// <summary>
/// Gettings for JWT authentication.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Secret key for JWT authentication.
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    /// Issuer for JWT authentication.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Audience for JWT authentication.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Expiration time for JWT authentication in minutes.
    /// </summary>
    public int ExpirationMinutes { get; set; }
}