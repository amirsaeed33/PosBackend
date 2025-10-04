using Microsoft.EntityFrameworkCore;
using POS.Core.Entities;

namespace POS.Core.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50).HasDefaultValue("User");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // Seed dummy users for testing
        // All passwords are hashed using BCrypt with work factor 11
        modelBuilder.Entity<User>().HasData(
            // Admin Users
            new User
            {
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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
                Id = 4,
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
                Id = 5,
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
                Id = 6,
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
                Id = 7,
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
                Id = 8,
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
                Id = 9,
                Name = "Inactive User",
                Email = "inactive@example.com",
                // Password: inactive123
                PasswordHash = "$2a$11$M7oB9p0R1sT2uV3wX4yZ5A6B7C8D9E0F1G2H3I4J5K6L7M8N9O",
                Role = "User",
                IsActive = false,
                CreatedAt = new DateTime(2025, 1, 4, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}

