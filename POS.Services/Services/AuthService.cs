using Microsoft.EntityFrameworkCore;
using POS.Core.Data;
using POS.Services.DTOs;
using POS.Services.Interfaces;
using BCrypt.Net;

namespace POS.Services.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            // Find user by email using AsNoTracking for read-only queries
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            // Check if user is active
            if (!user.IsActive)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Account is inactive. Please contact administrator."
                };
            }

            // Verify password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            // Return success response with user data
            return new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                User = new UserData
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role
                },
                Token = GenerateToken(user.Id, user.Email, user.Role)
            };
        }
        catch (Exception ex)
        {
            return new LoginResponse
            {
                Success = false,
                Message = $"An error occurred during login: {ex.Message}"
            };
        }
    }

    public async Task<bool> ValidateUserAsync(string email, string password)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);

        if (user == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }

    // Simple token generation (in production, use JWT)
    private string GenerateToken(int userId, string email, string role)
    {
        // For now, return a simple token
        // In production, implement proper JWT token generation
        var tokenData = $"{userId}:{email}:{role}:{DateTime.UtcNow.Ticks}";
        var tokenBytes = System.Text.Encoding.UTF8.GetBytes(tokenData);
        return Convert.ToBase64String(tokenBytes);
    }
}

