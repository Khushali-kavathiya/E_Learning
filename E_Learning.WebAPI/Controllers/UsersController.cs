using E_Learning.WebAPI.Contracts;
using E_Learning.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Learning.Services.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.JsonPatch;
using E_Learning.Extensions.Helpers;

namespace E_Learning.WebAPI.Controllers;

/// <summary>
/// UsersController class handles all user-related operations such as registration, login, profile updates,
/// fetching users, and deletion. It communicates with the UsersService to perform business logic.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]

public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public UsersController(IUsersService usersService, IMapper mapper)
    {
        _usersService = usersService;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers a new user with the provided user contract.
    /// </summary>
    /// <param name="contract">The UserContract object containing the user's registration details.</param>
    /// <returns>
    /// An IActionResult representing the result of the registration process.
    /// Returns a BadRequest with errors if registration fails, or an Ok result with a success message if registration succeeds.
    /// </returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserContract contract)
    {
        var model = _mapper.Map<UserModel>(contract);
        var result = await _usersService.RegisterAsync(model);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok($"{contract.Role} User registered successfully.");
    }


    /// <summary>
    /// Authenticates the user and returns a JWT token if credentials are valid.
    /// </summary>
    /// <param name="contract">The login request containing email and password.</param>
    /// <returns>
    /// Returns a 200 OK with a JWT token if login is successful;  
    /// otherwise, returns 401 Unauthorized with an error message.
    /// </returns>

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserContract contract)
    {
        var serviceRequest = _mapper.Map<UserModel>(contract);
        var token = await _usersService.LoginAsync(serviceRequest);

        if (token == null)
            return Unauthorized("Invalid credentials");

        return Ok(new { token });
    }

    /// <summary>
    /// Partially updates a user using JSON Patch.
    /// </summary>
    /// <param name="userId">User ID to update.</param>
    /// <param name="patchDoc">Patch operations on the user.</param>
    /// <returns>Updated user or error.</returns>
    [HttpPatch("{userId}")]
    [Authorize]
    public async Task<IActionResult> PatchUser(string userId, [FromBody] JsonPatchDocument<UserContract> patchDoc)
    {
        if (patchDoc == null)
            return BadRequest("Patch document is required.");

        //Get list of removed operations(for validation)
        var originalOpCount = patchDoc.Operations.Count;
        PatchFilter.RemoveCreateOnlyFields(patchDoc);
        if (patchDoc.Operations.Count == 0)
            return BadRequest("No updatable fields provided or all were restricted by CreateOnly attribute.");
        if (patchDoc.Operations.Count < originalOpCount)
            return BadRequest("Payload contains fields that cannot be update (e.g., Email or Role) Please remove them.");

        var user = await _usersService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound("User not found.");

        var contract = _mapper.Map<UserContract>(user);
        patchDoc.ApplyTo(contract, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedModel = _mapper.Map<UserModel>(contract);
        var result = await _usersService.PatchUserAsync(userId, updatedModel);

        return result.Succeeded ? Ok("User updated successfully.") : BadRequest(result.Errors);
    }


    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to retrieve.</param>
    /// <returns>
    /// An ActionResult representing the result of the user retrieval process.
    /// Returns a NotFound result if the user is not found, or an Ok result with the user's details if the user is found.
    /// </returns>
    [HttpGet("{userId}")]
    [Authorize]
    public async Task<ActionResult> GetUserById(string userId)
    {
        var user = await _usersService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound("User not found.");

        var response = _mapper.Map<UserContract>(user);
        return Ok(response);
    }


    /// <summary>
    /// Retrieves all users from the system. This endpoint is restricted to Admin roles only.
    /// </summary>
    /// <returns>
    /// An ActionResult containing a list of <see cref="UserContract"/> objects representing all users,
    /// or an error response if retrieval fails.
    /// </returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetAllUsers()
    {
        var users = await _usersService.GetAllUsersAsync();
        var response = _mapper.Map<List<UserContract>>(users);
        return Ok(response);
    }

    /// <summary>
    /// Deletes a user by their unique identifier. This endpoint is restricted to Admin roles only.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to delete.</param>
    /// <returns>
    /// An IActionResult with:
    /// - 200 OK and success message if deletion succeeds
    /// - 404 Not Found if user doesn't exist
    /// - 400 Bad Request with errors if deletion fails
    /// </returns>
    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUserById(string userId)
    {
        var result = await _usersService.DeleteUserByIdAsync(userId);

        if (result == null)
            return NotFound("User not found");

        return result.Succeeded ? Ok("User deleted successfully.") : BadRequest(result.Errors);
    }

}