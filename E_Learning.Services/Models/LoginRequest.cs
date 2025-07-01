using System.ComponentModel.DataAnnotations;

namespace E_Learning.Services.Models;

public class LoginRequest
{
    /// <summary>
    /// Represents a login request model containing user credentials.
    /// </summary>
   
    public string Email { get; set; }
    public string Password { get; set; }
}