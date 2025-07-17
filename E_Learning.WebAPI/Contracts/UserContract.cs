using E_Learning.Domain.Enums;
using E_Learning.Extensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.WebAPI.Contracts;

public class UserContract
{
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    [CreateOnly]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    [CreateOnly]
    public UserRole Role { get; set; }
}