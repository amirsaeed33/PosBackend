using POS.Services.DTOs;

namespace POS.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto?> GetProductBySkuAsync(string sku);
    Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto productDto);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<string>> GetCategoriesAsync();
}

