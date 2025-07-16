using System.Runtime.Intrinsics.Arm;
using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using E_Learning.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Implementations;

/// <summary>
/// Repository for handling user-related operations such as create, update, retrieve, and delete.
/// Uses ASP.NET Core Identity for user management.
/// </summary>
public class UsersRepository : IUsersRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly E_LearningDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersRepository"/> class.
    /// </summary>
    /// <param name="userManager">The ASP.NET Core Identity UserManager.</param>
    /// <param name="context">The EF Core database context.</param>
    public UsersRepository(UserManager<ApplicationUser> userManager, E_LearningDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    /// <inheritdoc />
    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    /// <inheritdoc />
    public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
    {
        return await _userManager.CheckPasswordAsync(applicationUser, password);
    }

    /// <inheritdoc />
    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
    {
        return await _userManager.UpdateAsync(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> RemovePasswordAsync(ApplicationUser user)
    {
        return await _userManager.RemovePasswordAsync(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
    {
        return await _userManager.AddPasswordAsync(user, password);
    }

    /// <inheritdoc />
    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteUserByIdAsync(ApplicationUser user)
    {
        return await _userManager.DeleteAsync(user);
    }
}
