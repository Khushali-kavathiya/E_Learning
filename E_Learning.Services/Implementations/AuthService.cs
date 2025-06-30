using E_Learning.Domain.Entities;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;


namespace E_Learning.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

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
}