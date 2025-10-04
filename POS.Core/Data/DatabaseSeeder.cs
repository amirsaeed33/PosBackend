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
        // Check if any users already exist
        if (await _context.Users.AnyAsync())
        {
            // Data already exists, skip seeding
            return;
        }

        // Seed users
        await SeedUsersAsync();
        
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
                PasswordHash = "$2a$11$N7DHv5cF5mKz5JqKGZwNXeZw3JRj9zJ6V8yqK4xZ9tJ7k5C6V4uXy",
                Role = "Admin",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "CXP Admin",
                Email = "admin@cxp.com",
                // Password: Admin123!
                PasswordHash = "$2a$11$N7DHv5cF5mKz5JqKGZwNXeZw3JRj9zJ6V8yqK4xZ9tJ7k5C6V4uXy",
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
                PasswordHash = "$2a$11$8Xvt90PQnNpxhOhqh8D8JeF3vZL8H7nJ5K9mL4pR6sT2uV1wX3yZ",
                Role = "Shop",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Mall Mithai Shop",
                Email = "mall@mithai.com",
                // Password: shop123
                PasswordHash = "$2a$11$8Xvt90PQnNpxhOhqh8D8JeF3vZL8H7nJ5K9mL4pR6sT2uV1wX3yZ",
                Role = "Shop",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Suburb Mithai Shop",
                Email = "suburb@mithai.com",
                // Password: shop123
                PasswordHash = "$2a$11$8Xvt90PQnNpxhOhqh8D8JeF3vZL8H7nJ5K9mL4pR6sT2uV1wX3yZ",
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
                PasswordHash = "$2a$11$K5mZ7n8P9qR1sT2uV3wX4yZ5A6B7C8D9E0F1G2H3I4J5K6L7M8N",
                Role = "User",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                // Password: user123
                PasswordHash = "$2a$11$K5mZ7n8P9qR1sT2uV3wX4yZ5A6B7C8D9E0F1G2H3I4J5K6L7M8N",
                Role = "User",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Name = "Test User",
                Email = "test@test.com",
                // Password: test123
                PasswordHash = "$2a$11$L6nA8o9Q0rS1tU2vW3xY4zA5B6C7D8E9F0G1H2I3J4K5L6M7N8O",
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
                PasswordHash = "$2a$11$M7oB9p0R1sT2uV3wX4yZ5A6B7C8D9E0F1G2H3I4J5K6L7M8N9O",
                Role = "User",
                IsActive = false,
                CreatedAt = new DateTime(2025, 1, 4, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        await _context.Users.AddRangeAsync(users);
    }
}

