using Microsoft.AspNetCore.Mvc;
using POS.Services.DTOs;
using POS.Services.Interfaces;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid request data",
                });
            }

            var response = await _authService.LoginAsync(request);

            if (!response.Success)
            {
                return Ok(response); // Return 200 with success: false
            }

            _logger.LogInformation("User {Email} logged in successfully", request.Email);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for {Email}", request.Email);
            return StatusCode(500, new LoginResponse
            {
                Success = false,
                Message = "An internal server error occurred"
            });
        }
    }


    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }
}

