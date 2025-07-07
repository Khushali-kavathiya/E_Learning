using E_Learning.Services.Interface;
using Microsoft.AspNetCore.Identity;
using E_Learning.Services.Models;
using E_Learning.Repositories.Interface;
using E_Learning.Services.Mapping;
using E_Learning.Services.Interfaces;
using E_Learning.Domain.Entities;
using AutoMapper;

namespace E_Learning.Services.Implementations;

/// <summary>
/// AuthService class implements IAuthService interface for user authentication and registration.
/// It provides methods for user registration and login.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IAuthRepository _userReposity;
    private readonly IJwtTokensService _jwtTokensService;
    private readonly IMapper _mapper;

    public AuthService(IAuthRepository usersRepository, IJwtTokensService jwtTokensService, IMapper mapper)
    {
        _userReposity = usersRepository;
        _jwtTokensService = jwtTokensService;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers a new user with the provided model.
    /// Maps the model to an entity, creates the user, and adds them to a specified role.
    /// </summary>
    /// <param name="model">The model containing user registration details.</param>
    /// <returns>IdentityResult indicating the success or failure of the registration.</returns>
    public async Task<IdentityResult> RegisterAsync(RegisterUserModel model)
    {
        var userEntity = _mapper.Map<ApplicationUser>(model);

        var result = await _userReposity.CreateUserAsync(userEntity, model.Password);
        if (!result.Succeeded)
            return result;

        return await _userReposity.AddUserToRoleAsync(userEntity, model.Role.ToString());
    }

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// Validates the user's email and password, and generates a JWT token if successful.
    /// </summary>
    /// <param name="model">The model containing user login details.</param>
    /// <returns>A JWT token if the login is successful; otherwise, throws an UnauthorizedAccessException.</returns>
    public async Task<string> LoginAsync(LoginRequestModel model)
    {
        var user = await _userReposity.GetUserByEmailAsync(model.Email);
        if (user == null || !await _userReposity.CheckPasswordAsync(user, model.Password))
            throw new UnauthorizedAccessException("Invalid Credentials");

        return _jwtTokensService.GenerateToken(user);
    }
}