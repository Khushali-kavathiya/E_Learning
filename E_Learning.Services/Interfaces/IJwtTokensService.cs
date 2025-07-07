using E_Learning.Domain.Entities;

namespace E_Learning.Services.Interfaces;

public interface IJwtTokensService
{
    /// <summary>
    /// Generates a JWT token for the specified application user.
    /// </summary>
    /// <param name="applicationUser">The application user for whom the token is generated.</param>
    /// <returns>A JWT token as a string.</returns>
    string GenerateToken(ApplicationUser applicationUser);
}