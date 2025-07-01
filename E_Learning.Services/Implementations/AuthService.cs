using E_Learning.Domain.Entities;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace E_Learning.Services.Implementation;

/// <summary>
/// Provides authentication-related operations such as user registration and login.
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class with dependencies.
    /// </summary>
    /// <param name="userManager">User manager for Identity operations.</param>
    /// <param name="roleManager">Role manager for managing user roles.</param>
    /// <param name="mapper">AutoMapper instance for model mapping.</param>
    /// <param name="configuration">Application configuration for JWT settings.</param>
    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _configuration = configuration;
    }


    /// <summary>
    /// Registers a new user with a specified or default role.
    /// </summary>
    /// <param name="request">The registration request containing user details.</param>
    /// <returns>result of the registration.</returns>
    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return "User already exists.";

        var user = _mapper.Map<ApplicationUser>(request);

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));

        var roleToAssign = string.IsNullOrWhiteSpace(request.Role) ? "student" : request.Role;

        if (!await _roleManager.RoleExistsAsync(roleToAssign))
            await _roleManager.CreateAsync(new IdentityRole(roleToAssign));

        await _userManager.AddToRoleAsync(user, roleToAssign);

        return $"User registered successfully as {roleToAssign}.";

    }


    /// <summary>
    /// Authenticates a user and generates a JWT token with role claims.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <returns>JWT token string if authentication is successful.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if credentials are invalid.</exception>
    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        var role = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(role.Select(role => new Claim(ClaimTypes.Role, role)));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();
        var mappedUsers = _mapper.Map<List<UserResponse>>(users);

        for (int i = 0; i < users.Count; i++)
        {
            var roles = await _userManager.GetRolesAsync(users[i]);
            mappedUsers[i].Roles = roles;
        }
        return mappedUsers;
    }


    /// <summary>
    /// Retrieves a user by email and maps to UserResponse with roles.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    public async Task<UserResponse> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new Exception("User not found");
        var mappedUsers = _mapper.Map<UserResponse>(user);

        var role = await _userManager.GetRolesAsync(user);
        mappedUsers.Roles = role;

        return mappedUsers;
    }

}