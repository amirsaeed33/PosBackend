using POS.Services.DTOs;

namespace POS.Services.Interfaces;

public interface IShopService
{
    Task<IEnumerable<ShopDto>> GetAllShopsAsync();
    Task<ShopDto?> GetShopByIdAsync(int id);
    Task<ShopDto?> GetShopByEmailAsync(string email);
    Task<ShopDto?> GetShopByUserIdAsync(int userId);
    Task<ShopDto> CreateShopAsync(CreateShopDto shopDto);
    Task<ShopDto?> UpdateShopAsync(int id, UpdateShopDto shopDto);
    Task<bool> DeleteShopAsync(int id);
    Task<bool> UpdateBalanceAsync(int id, decimal amount);
}

