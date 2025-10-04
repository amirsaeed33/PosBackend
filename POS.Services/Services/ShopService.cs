using Microsoft.EntityFrameworkCore;
using POS.Core.Data;
using POS.Core.Entities;
using POS.Services.DTOs;
using POS.Services.Interfaces;

namespace POS.Services.Services;

public class ShopService : IShopService
{
    private readonly AppDbContext _context;

    public ShopService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShopDto>> GetAllShopsAsync()
    {
        var shops = await _context.Shops
            .AsNoTracking()
            .Include(s => s.User)
            .Where(s => s.IsActive)
            .OrderBy(s => s.Name)
            .ToListAsync();

        return shops.Select(MapToDto);
    }

    public async Task<ShopDto?> GetShopByIdAsync(int id)
    {
        var shop = await _context.Shops
            .AsNoTracking()
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        return shop != null ? MapToDto(shop) : null;
    }

    public async Task<ShopDto?> GetShopByEmailAsync(string email)
    {
        var shop = await _context.Shops
            .AsNoTracking()
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Email == email);

        return shop != null ? MapToDto(shop) : null;
    }

    public async Task<ShopDto?> GetShopByUserIdAsync(int userId)
    {
        var shop = await _context.Shops
            .AsNoTracking()
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.UserId == userId);

        return shop != null ? MapToDto(shop) : null;
    }

    public async Task<ShopDto> CreateShopAsync(CreateShopDto shopDto)
    {
        // Create User account for the shop
        var user = new User
        {
            Name = shopDto.Name,
            Email = shopDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(shopDto.Password, 11),
            Role = "Shop",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Create Shop
        var shop = new Shop
        {
            Name = shopDto.Name,
            Email = shopDto.Email,
            Phone = shopDto.Phone,
            Address = shopDto.Address,
            ContactPerson = shopDto.ContactPerson,
            City = shopDto.City,
            State = shopDto.State,
            ZipCode = shopDto.ZipCode,
            Balance = shopDto.Balance,
            UserId = user.Id,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Shops.Add(shop);
        await _context.SaveChangesAsync();

        shop.User = user;
        return MapToDto(shop);
    }

    public async Task<ShopDto?> UpdateShopAsync(int id, UpdateShopDto shopDto)
    {
        var shop = await _context.Shops
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (shop == null)
            return null;

        // Update only provided fields
        if (shopDto.Name != null)
        {
            shop.Name = shopDto.Name;
            if (shop.User != null)
                shop.User.Name = shopDto.Name;
        }

        if (shopDto.Phone != null)
            shop.Phone = shopDto.Phone;

        if (shopDto.Address != null)
            shop.Address = shopDto.Address;

        if (shopDto.ContactPerson != null)
            shop.ContactPerson = shopDto.ContactPerson;

        if (shopDto.City != null)
            shop.City = shopDto.City;

        if (shopDto.State != null)
            shop.State = shopDto.State;

        if (shopDto.ZipCode != null)
            shop.ZipCode = shopDto.ZipCode;

        if (shopDto.Balance.HasValue)
            shop.Balance = shopDto.Balance.Value;

        if (shopDto.IsActive.HasValue)
        {
            shop.IsActive = shopDto.IsActive.Value;
            if (shop.User != null)
                shop.User.IsActive = shopDto.IsActive.Value;
        }

        shop.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(shop);
    }

    public async Task<bool> DeleteShopAsync(int id)
    {
        var shop = await _context.Shops
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (shop == null)
            return false;

        // Soft delete
        shop.IsActive = false;
        shop.UpdatedAt = DateTime.UtcNow;

        // Also deactivate the user account
        if (shop.User != null)
        {
            shop.User.IsActive = false;
            shop.User.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateBalanceAsync(int id, decimal amount)
    {
        var shop = await _context.Shops.FindAsync(id);

        if (shop == null)
            return false;

        shop.Balance += amount;
        shop.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    private static ShopDto MapToDto(Shop shop)
    {
        return new ShopDto
        {
            Id = shop.Id,
            Name = shop.Name,
            Email = shop.Email,
            Phone = shop.Phone,
            Address = shop.Address,
            Balance = shop.Balance,
            ContactPerson = shop.ContactPerson,
            City = shop.City,
            State = shop.State,
            ZipCode = shop.ZipCode,
            IsActive = shop.IsActive,
            CreatedAt = shop.CreatedAt,
            UpdatedAt = shop.UpdatedAt,
            UserId = shop.UserId,
            UserEmail = shop.User?.Email
        };
    }
}

