using E_Learning.WebAPI.Contracts;
using E_Learning.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Controllers;

// <summary>
// AuthController class handles user authentication and registration.
// It provides endpoints for user registration and login.
// </summary>
[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers a new user with the provided contract.
    /// Maps the contract to a model, creates the user, and adds them to a specified role.
    /// </summary>
    /// <param name="contract">The contract containing user registration details.</param>
    /// <returns>Returns a success message if registration is successful, otherwise returns a bad request with errors.</returns>

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserContract contract)
    {
        var model = _mapper.Map<RegisterUserModel>(contract);
        var result = await _authService.RegisterAsync(model);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok($"{contract.Role} User registered successfully.");
    }

    /// <summary>
    /// Logs in a user with the provided contract.
    /// Validates the user's email and password, and generates a JWT token if successful.
    /// </summary>
    /// <param name="contract">The contract containing user login details.</param>
    /// <returns>Returns a JWT token if login is successful, otherwise returns an unauthorized response with an error message.</returns>

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest contract)
    {
        var serviceRequest = _mapper.Map<LoginRequestModel>(contract);
        var token = await _authService.LoginAsync(serviceRequest);

        if (token == null)
            return Unauthorized("Invalid credentials");

        return Ok(new { token });    
    }
}