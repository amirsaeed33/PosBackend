using Microsoft.EntityFrameworkCore;
using POS.Core.Data;
using POS.Core.Entities;
using POS.Services.DTOs;
using POS.Services.Interfaces;

namespace POS.Services.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .Where(p => p.IsActive)
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Name)
            .ToListAsync();

        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && p.Category == category)
            .OrderBy(p => p.Name)
            .ToListAsync();

        return products.Select(MapToDto);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        return product != null ? MapToDto(product) : null;
    }

    public async Task<ProductDto?> GetProductBySkuAsync(string sku)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.SKU == sku);

        return product != null ? MapToDto(product) : null;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Category = productDto.Category,
            Price = productDto.Price,
            Stock = productDto.Stock,
            SKU = productDto.SKU,
            Description = productDto.Description,
            Image = productDto.Image,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);
        
        if (product == null)
            return null;

        // Update only provided fields
        if (productDto.Name != null)
            product.Name = productDto.Name;
        
        if (productDto.Category != null)
            product.Category = productDto.Category;
        
        if (productDto.Price.HasValue)
            product.Price = productDto.Price.Value;
        
        if (productDto.Stock.HasValue)
            product.Stock = productDto.Stock.Value;
        
        if (productDto.Description != null)
            product.Description = productDto.Description;
        
        if (productDto.Image != null)
            product.Image = productDto.Image;
        
        if (productDto.IsActive.HasValue)
            product.IsActive = productDto.IsActive.Value;

        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        
        if (product == null)
            return false;

        // Soft delete
        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => p.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            Stock = product.Stock,
            SKU = product.SKU,
            Description = product.Description,
            Image = product.Image,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}

