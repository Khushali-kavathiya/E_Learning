using E_Learning.Domain.Enums;
using E_Learning.Extensions.Attributes;
using E_Learning.Extensions.Mappings;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Contracts;

/// <summary>
/// UserContract class for creating a user.
/// </summary>
public class UserContract : IMapFrom<UserModel>
{
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    [CreateOnly]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    [CreateOnly]
    public UserRole? Role { get; set; }
}