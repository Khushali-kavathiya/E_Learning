using System.Xml.Schema;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.WenAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Public Registraion for students
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost("register-student")]
    // [AllowAnonymous]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterRequest request)
    {
        request.Role = "Student";
        var result = await _authService.RegisterAsync(request);
        return Ok(new { message = result });
    }


    /// <summary>
    /// Admin-only registration for instructor
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost("register-instructor")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterInstructor([FromBody] RegisterRequest request)
    {
        request.Role = "Instructor";
        var result = await _authService.RegisterAsync(request);
        return Ok(new { message = result });
    }

    /// <summary>
    /// Admin-only registration for admins
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost("register-admin")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
    {
        request.Role = "Admin";
        var result = await _authService.RegisterAsync(request);
        return Ok(new { message = result });
    }


    /// <summary>
    /// Handles authentication-related endpoints.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _authService.LoginAsync(request);
            return Ok(token);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid email or Password");
        }
    }
}
