using Microsoft.AspNetCore.Mvc;
using POS.Services.DTOs;
using POS.Services.Interfaces;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopsController : ControllerBase
{
    private readonly IShopService _shopService;
    private readonly ILogger<ShopsController> _logger;

    public ShopsController(IShopService shopService, ILogger<ShopsController> logger)
    {
        _shopService = shopService;
        _logger = logger;
    }

    /// <summary>
    /// Get all active shops
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShopDto>>> GetAllShops()
    {
        try
        {
            var shops = await _shopService.GetAllShopsAsync();
            return Ok(shops);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all shops");
            return StatusCode(500, new { message = "An error occurred while retrieving shops" });
        }
    }

    /// <summary>
    /// Get shop by ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ShopDto>> GetShopById(int id)
    {
        try
        {
            var shop = await _shopService.GetShopByIdAsync(id);

            if (shop == null)
                return NotFound(new { message = $"Shop with ID {id} not found" });

            return Ok(shop);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving shop {ShopId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the shop" });
        }
    }

    /// <summary>
    /// Get shop by email
    /// </summary>
    [HttpGet("email/{email}")]
    public async Task<ActionResult<ShopDto>> GetShopByEmail(string email)
    {
        try
        {
            var shop = await _shopService.GetShopByEmailAsync(email);

            if (shop == null)
                return NotFound(new { message = $"Shop with email {email} not found" });

            return Ok(shop);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving shop with email {Email}", email);
            return StatusCode(500, new { message = "An error occurred while retrieving the shop" });
        }
    }

    /// <summary>
    /// Get shop by user ID
    /// </summary>
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<ShopDto>> GetShopByUserId(int userId)
    {
        try
        {
            var shop = await _shopService.GetShopByUserIdAsync(userId);

            if (shop == null)
                return NotFound(new { message = $"Shop for user ID {userId} not found" });

            return Ok(shop);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving shop for user {UserId}", userId);
            return StatusCode(500, new { message = "An error occurred while retrieving the shop" });
        }
    }

    /// <summary>
    /// Create a new shop (with user account)
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ShopDto>> CreateShop([FromBody] CreateShopDto shopDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shop = await _shopService.CreateShopAsync(shopDto);
            return CreatedAtAction(nameof(GetShopById), new { id = shop.Id }, shop);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating shop");
            return StatusCode(500, new { message = "An error occurred while creating the shop" });
        }
    }

    /// <summary>
    /// Update an existing shop
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ShopDto>> UpdateShop(int id, [FromBody] UpdateShopDto shopDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shop = await _shopService.UpdateShopAsync(id, shopDto);

            if (shop == null)
                return NotFound(new { message = $"Shop with ID {id} not found" });

            return Ok(shop);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating shop {ShopId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the shop" });
        }
    }

    /// <summary>
    /// Delete a shop (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShop(int id)
    {
        try
        {
            var result = await _shopService.DeleteShopAsync(id);

            if (!result)
                return NotFound(new { message = $"Shop with ID {id} not found" });

            return Ok(new { message = "Shop deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting shop {ShopId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the shop" });
        }
    }

    /// <summary>
    /// Update shop balance
    /// </summary>
    [HttpPatch("{id}/balance")]
    public async Task<ActionResult> UpdateBalance(int id, [FromBody] decimal amount)
    {
        try
        {
            var result = await _shopService.UpdateBalanceAsync(id, amount);

            if (!result)
                return NotFound(new { message = $"Shop with ID {id} not found" });

            return Ok(new { message = "Balance updated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating balance for shop {ShopId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the balance" });
        }
    }

    /// <summary>
    /// Health check endpoint
    /// </summary>
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }
}

