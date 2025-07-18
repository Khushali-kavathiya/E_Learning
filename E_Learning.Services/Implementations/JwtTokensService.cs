using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Learning.Domain.Entities;
using E_Learning.Services.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
namespace E_Learning.Services.Interfaces;

public class JwtTokensService(JwtSettings _jwtSettings, UserManager<ApplicationUser> _userManager) : IJwtTokensService
{
    /// <summary>
    /// Generates a JWT token for the specified application user.
    /// </summary>
    /// <param name="applicationUser">The application user for whom the token is generated.</param>
    /// <returns>A JWT token as a string.</returns>
    public string GenerateToken(ApplicationUser applicationUser)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
            new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = _userManager.GetRolesAsync(applicationUser).Result;
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}