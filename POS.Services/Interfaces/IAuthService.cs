using POS.Services.DTOs;

namespace POS.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<bool> ValidateUserAsync(string email, string password);
}

