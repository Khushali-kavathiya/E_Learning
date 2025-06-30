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

    [HttpPost("register-student")]
   // [AllowAnonymous]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterRequest request)
    {
        request.Role = "Student";
        var result = await _authService.RegisterAsync(request);
        return Ok(new { message = result });
    }

    [HttpPost("register-instructor")]
   // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterInstructor([FromBody] RegisterRequest request)
    {
        request.Role = "Instructor";
        var result = await _authService.RegisterAsync(request);
        return Ok(new { message = result });
    }

        // Admin-only registration for admins
    [HttpPost("register-admin")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
    {
            request.Role = "Admin";
            var result = await _authService.RegisterAsync(request);
            return Ok(new { message = result });
    }
}
