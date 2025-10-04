using Microsoft.EntityFrameworkCore;
using POS.Core.Entities;

namespace POS.Core.Data;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;

    public DatabaseSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Seed users if not exists
        if (!await _context.Users.AnyAsync())
        {
            await SeedUsersAsync();
        }

        // Seed products if not exists
        if (!await _context.Products.AnyAsync())
        {
            await SeedProductsAsync();
        }
        
        // Save all changes
        await _context.SaveChangesAsync();
    }

    private async Task SeedUsersAsync()
    {
        var users = new List<User>
        {
            // Admin Users
            new User
            {
                Name = "Admin User",
                Email = "admin@pos.com",
                // Password: Admin123!
                PasswordHash = "$2a$11$LclUW..6W4CJ4cQPEp499.Rr9WTM6WoIPzRMu75LARhLuAnL0SmPe",
                Role = "Admin",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "CXP Admin",
                Email = "admin@cxp.com",
                // Password: Admin123!
                PasswordHash = "$2a$11$LclUW..6W4CJ4cQPEp499.Rr9WTM6WoIPzRMu75LARhLuAnL0SmPe",
                Role = "Admin",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Shop Users
            new User
            {
                Name = "Downtown Mithai Shop",
                Email = "downtown@mithai.com",
                // Password: shop123
                PasswordHash = "$2a$11$fh2hAkA1ryQZZeoleEWrG.x.P4.CzF7DqXO5HgoLS65cnwGfTxAvu",
                Role = "Shop",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Mall Mithai Shop",
                Email = "mall@mithai.com",
                // Password: shop123
                PasswordHash = "$2a$11$fh2hAkA1ryQZZeoleEWrG.x.P4.CzF7DqXO5HgoLS65cnwGfTxAvu",
                Role = "Shop",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Suburb Mithai Shop",
                Email = "suburb@mithai.com",
                // Password: shop123
                PasswordHash = "$2a$11$fh2hAkA1ryQZZeoleEWrG.x.P4.CzF7DqXO5HgoLS65cnwGfTxAvu",
                Role = "Shop",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Regular Users
            new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                // Password: user123
                PasswordHash = "$2a$11$fB7UR9QXm1fvpu163tYqW.3q63CLkLv6gXQs3HaOwUxmgVpHj0O4a",
                Role = "User",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                // Password: user123
                PasswordHash = "$2a$11$fB7UR9QXm1fvpu163tYqW.3q63CLkLv6gXQs3HaOwUxmgVpHj0O4a",
                Role = "User",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Test User",
                Email = "test@test.com",
                // Password: test123
                PasswordHash = "$2a$11$ws1wxpVe4UQPIngiD5/tIuCTPQgXA1d9/5ORpdbgQHODIR4MLJ5/S",
                Role = "User",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // Inactive User (for testing)
            new User
            {
                Name = "Inactive User",
                Email = "inactive@example.com",
                // Password: inactive123
                PasswordHash = "$2a$11$7ZixXkJwUsM7EU.G8oJtOuUTSEuFmUtaeMgjTgwWhr78B4iXHCuRK",
                Role = "User",
                IsActive = false,
                CreatedAt = new DateTime(2025, 1, 4, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        await _context.Users.AddRangeAsync(users);
    }

    private async Task SeedProductsAsync()
    {
        var products = new List<Product>
        {
            // Milk Based Sweets
            new Product { Name = "Gulab Jamun", Category = "Milk Based", Price = 120, Stock = 150, SKU = "MILK-GJ-001", Description = "Soft milk solid balls soaked in sugar syrup, 250g", Image = "🟤", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Rasgulla", Category = "Milk Based", Price = 140, Stock = 120, SKU = "MILK-RG-002", Description = "Soft spongy balls made from chhena and soaked in syrup, 250g", Image = "⚪", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Rasmalai", Category = "Milk Based", Price = 180, Stock = 100, SKU = "MILK-RM-003", Description = "Soft paneer discs in sweetened milk, 250g", Image = "🥛", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Kheer (Rice Pudding)", Category = "Milk Based", Price = 100, Stock = 80, SKU = "MILK-KH-004", Description = "Traditional rice pudding with milk and dry fruits, 250g", Image = "🍚", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Barfi
            new Product { Name = "Kaju Katli", Category = "Barfi", Price = 450, Stock = 150, SKU = "BARF-KK-005", Description = "Premium cashew barfi with silver leaf, 250g", Image = "🔶", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Coconut Barfi", Category = "Barfi", Price = 200, Stock = 180, SKU = "BARF-CB-006", Description = "Sweet coconut barfi, 250g", Image = "🥥", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Milk Barfi", Category = "Barfi", Price = 160, Stock = 200, SKU = "BARF-MB-007", Description = "Classic milk barfi, 250g", Image = "🔶", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Kesar Barfi", Category = "Barfi", Price = 280, Stock = 100, SKU = "BARF-KB-008", Description = "Saffron flavored premium barfi, 250g", Image = "🟨", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Ladoo
            new Product { Name = "Motichoor Ladoo", Category = "Ladoo", Price = 240, Stock = 250, SKU = "LADU-ML-009", Description = "Tiny gram flour balls shaped into ladoos, 250g", Image = "🟠", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Besan Ladoo", Category = "Ladoo", Price = 200, Stock = 220, SKU = "LADU-BL-010", Description = "Roasted gram flour ladoos with ghee, 250g", Image = "🟡", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Rava Ladoo", Category = "Ladoo", Price = 180, Stock = 200, SKU = "LADU-RL-011", Description = "Semolina ladoos with dry fruits, 250g", Image = "⚪", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Boondi Ladoo", Category = "Ladoo", Price = 220, Stock = 180, SKU = "LADU-BD-012", Description = "Sweet boondi balls with cardamom, 250g", Image = "🟠", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Peda
            new Product { Name = "Mathura Peda", Category = "Peda", Price = 260, Stock = 150, SKU = "PEDA-MP-013", Description = "Famous Mathura style milk peda, 250g", Image = "🟤", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Kesar Peda", Category = "Peda", Price = 300, Stock = 120, SKU = "PEDA-KP-014", Description = "Saffron flavored peda, 250g", Image = "🟨", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Chocolate Peda", Category = "Peda", Price = 240, Stock = 160, SKU = "PEDA-CP-015", Description = "Modern chocolate flavored peda, 250g", Image = "🟤", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Fried Sweets
            new Product { Name = "Jalebi", Category = "Fried Sweets", Price = 140, Stock = 200, SKU = "FRIE-JL-016", Description = "Crispy spiral shaped sweet soaked in sugar syrup, 250g", Image = "🟠", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Imarti", Category = "Fried Sweets", Price = 160, Stock = 150, SKU = "FRIE-IM-017", Description = "Flower shaped crispy sweet, 250g", Image = "🔴", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Balushahi", Category = "Fried Sweets", Price = 180, Stock = 130, SKU = "FRIE-BL-018", Description = "Flaky fried sweet with sugar glaze, 250g", Image = "🟤", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Bengali Sweets
            new Product { Name = "Sandesh", Category = "Bengali Sweets", Price = 220, Stock = 100, SKU = "BENG-SD-019", Description = "Traditional Bengali sweet made from chhena, 250g", Image = "🟡", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Cham Cham", Category = "Bengali Sweets", Price = 200, Stock = 90, SKU = "BENG-CC-020", Description = "Cylindrical chhena sweet coated with khoya, 250g", Image = "🟨", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Mishti Doi", Category = "Bengali Sweets", Price = 80, Stock = 150, SKU = "BENG-MD-021", Description = "Sweet yogurt, 250g container", Image = "🥛", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Dry Fruits
            new Product { Name = "Dry Fruit Halwa", Category = "Dry Fruits", Price = 320, Stock = 80, SKU = "DFRUT-DH-022", Description = "Rich halwa with assorted dry fruits, 250g", Image = "🌰", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Anjeer Barfi", Category = "Dry Fruits", Price = 380, Stock = 70, SKU = "DFRUT-AB-023", Description = "Fig and dry fruit barfi, 250g", Image = "🟫", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Pista Roll", Category = "Dry Fruits", Price = 420, Stock = 60, SKU = "DFRUT-PR-024", Description = "Pistachio wrapped in sweet khoya, 250g", Image = "🟢", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Namkeen
            new Product { Name = "Mixture Namkeen", Category = "Namkeen", Price = 80, Stock = 300, SKU = "NAMK-MX-025", Description = "Spicy mixed namkeen, 250g", Image = "🥨", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Sev Bhujia", Category = "Namkeen", Price = 60, Stock = 350, SKU = "NAMK-SB-026", Description = "Crispy gram flour noodles, 250g", Image = "🟡", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Mathri", Category = "Namkeen", Price = 70, Stock = 280, SKU = "NAMK-MT-027", Description = "Flaky savory crackers, 250g", Image = "🟤", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Special Mithai
            new Product { Name = "Raj Bhog", Category = "Special Mithai", Price = 280, Stock = 80, SKU = "SPEC-RB-028", Description = "Large rasgulla stuffed with dry fruits and saffron, 250g", Image = "✨", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Kalakand", Category = "Special Mithai", Price = 240, Stock = 100, SKU = "SPEC-KK-029", Description = "Soft milk cake sweet, 250g", Image = "⬜", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Mysore Pak", Category = "Special Mithai", Price = 300, Stock = 90, SKU = "SPEC-MP-030", Description = "South Indian gram flour fudge with ghee, 250g", Image = "🟨", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            
            // Gift Boxes
            new Product { Name = "Assorted Mithai Box", Category = "Gift Boxes", Price = 500, Stock = 50, SKU = "GIFT-AM-031", Description = "Mixed varieties of traditional sweets, 500g", Image = "🎁", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Premium Dry Fruit Box", Category = "Gift Boxes", Price = 800, Stock = 30, SKU = "GIFT-DF-032", Description = "Luxury dry fruit sweets gift box, 500g", Image = "🎁", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Diwali Special Hamper", Category = "Gift Boxes", Price = 1200, Stock = 25, SKU = "GIFT-DH-033", Description = "Premium festival hamper with assorted mithai, 1kg", Image = "🪔", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Wedding Gift Box", Category = "Gift Boxes", Price = 1500, Stock = 20, SKU = "GIFT-WB-034", Description = "Elegant wedding gift box with premium sweets, 1kg", Image = "💝", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Name = "Kaju Katli Premium Box", Category = "Gift Boxes", Price = 900, Stock = 35, SKU = "GIFT-KK-035", Description = "Pure kaju katli in decorative box, 500g", Image = "🎁", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        };

        await _context.Products.AddRangeAsync(products);
    }
}

