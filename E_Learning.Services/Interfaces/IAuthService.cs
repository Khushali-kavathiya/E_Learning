using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest request);
}