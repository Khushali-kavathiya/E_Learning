using E_Learning.Domain.Enums;

namespace E_Learning.Services.Models;

public class RegisterUserModel
{
    /// <summary>
    /// Email of the User.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Password of the User.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// FullName of the User.
    /// </summary>       
    public string FullName { get; set; }

    /// <summary>
    /// Role of the User.
    /// </summary>
    public UserRole Role { get; set; }
}