using E_Learning.Services.Interface;
using Microsoft.AspNetCore.Identity;
using E_Learning.Services.Models;
using E_Learning.Repositories.Interface;
using E_Learning.Services.Mapping;
using E_Learning.Services.Interfaces;
using E_Learning.Domain.Entities;
using AutoMapper;
using E_Learning.Domain.Enums;

namespace E_Learning.Services.Implementations;

/// <summary>
/// userService class implements IuserService interface for user authentication and registration.
/// It provides methods for user registration and login.
/// </summary>
public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokensService _jwtTokensService;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IJwtTokensService jwtTokensService, IMapper mapper)
    {
         _usersRepository = usersRepository;
        _jwtTokensService = jwtTokensService;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers a new user with the provided model.
    /// Maps the model to an entity, creates the user, and adds them to a specified role.
    /// </summary>
    /// <param name="model">The model containing user registration details.</param>
    /// <returns>IdentityResult indicating the success or failure of the registration.</returns>
    public async Task<IdentityResult> RegisterAsync(UserModel model)
    {
        var userEntity = _mapper.Map<ApplicationUser>(model);

        var result = await  _usersRepository.CreateUserAsync(userEntity, model.Password);
        if (!result.Succeeded)
            return result;

        return await  _usersRepository.AddUserToRoleAsync(userEntity, model.Role.ToString());
    }

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// Validates the user's email and password, and generates a JWT token if successful.
    /// </summary>
    /// <param name="model">The model containing user login details.</param>
    /// <returns>A JWT token if the login is successful; otherwise, throws an UnauthorizedAccessException.</returns>
    public async Task<string> LoginAsync(UserModel model)
    {
        var user = await  _usersRepository.GetUserByEmailAsync(model.Email);
        if (user == null || !await  _usersRepository.CheckPasswordAsync(user, model.Password))
            throw new UnauthorizedAccessException("Invalid Credentials");

        return _jwtTokensService.GenerateToken(user);
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// Includes role mapping for role-aware features.
    /// </summary>
    /// <param name="userId">The user ID to retrieve.</param>
    /// <returns>
    /// The corresponding <see cref="UserModel"/> if found; otherwise, null.
    /// </returns>

    public async Task<UserModel> GetUserByIdAsync(string userId)
    {
        var user = await _usersRepository.GetUserByIdAsync(userId);
        if (user == null)
            return null;
        var roles = await _usersRepository.GetUserRolesAsync(user);
        var roleName = roles.FirstOrDefault() ?? "Student";

        Enum.TryParse<UserRole>(roleName, ignoreCase: true, out var parseRole);

        var userModel = _mapper.Map<UserModel>(user);
        userModel.Role = parseRole;

        return userModel;
    }

    /// <summary>
    /// Partially updates user data using a provided patch model.
    /// Handles field-level updates including name, email, and password.
    /// </summary>
    /// <param name="id">The user ID to patch.</param>
    /// <param name="patchModel">The user model with updated fields.</param>
    /// <returns>
    /// <see cref="IdentityResult"/> indicating success or failure of the update.
    /// </returns>
    public async Task<IdentityResult> PatchUserAsync(string id, UserModel patchModel)
    {
        // Tracked instance
        var existingUser = await  _usersRepository.GetUserByIdAsync(id);
        if (existingUser is null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        // Directly update the tracked instance
        if (!string.IsNullOrWhiteSpace(patchModel.Email) &&
            !string.Equals(patchModel.Email, existingUser.Email, StringComparison.OrdinalIgnoreCase))
        {
            var userWithSameEmail = await  _usersRepository.GetUserByEmailAsync(patchModel.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != id)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already taken." });
            }

            existingUser.Email = patchModel.Email;
            existingUser.UserName = patchModel.Email;
        }

        if (!string.IsNullOrWhiteSpace(patchModel.FullName))
        {
            existingUser.FullName = patchModel.FullName;
        }

        if (!string.IsNullOrWhiteSpace(patchModel.Password))
        {
            var remove = await  _usersRepository.RemovePasswordAsync(existingUser);
            if (!remove.Succeeded)
                return remove;

            var add = await  _usersRepository.AddPasswordAsync(existingUser, patchModel.Password);
            if (!add.Succeeded)
                return add;
        }

        // Do NOT remap! Just update the tracked entity
        return await  _usersRepository.UpdateUserAsync(existingUser);
    }

    /// <summary>
    /// Retrieves all users in the system with their roles mapped to <see cref="UserRole"/>.
    /// </summary>
    /// <returns>
    /// A list of <see cref="UserModel"/> representing all users.
    /// </returns>
    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        var users = await _usersRepository.GetAllUsersAsync();
        var userModels = new List<UserModel>();

        foreach (var user in users)
        {
            var roles = await _usersRepository.GetUserRolesAsync(user);
            var roleName = roles.FirstOrDefault() ?? "student";

            Enum.TryParse<UserRole>(roleName, ignoreCase: true, out var parsedRole);

            var model = _mapper.Map<UserModel>(user);
            model.Role = parsedRole;

            userModels.Add(model);
        }
        return userModels;
    }

    /// <summary>
    /// Deletes a user identified by their user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>
    /// An <see cref="IdentityResult"/> indicating success or failure. Null if user is not found.
    /// </returns>
    public async Task<IdentityResult> DeleteUserByIdAsync(string userId)
    {
        var user = await _usersRepository.GetUserByIdAsync(userId);
        if (user == null)
            return null;

        return await _usersRepository.DeleteUserByIdAsync(user);

    }
}