Console.WriteLine("=== BCrypt Password Hash Generator ===\n");

var passwords = new Dictionary<string, string>
{
    { "Admin123!", "Admin password" },
    { "shop123", "Shop password" },
    { "user123", "User password" },
    { "test123", "Test password" },
    { "inactive123", "Inactive password" }
};

foreach (var kvp in passwords)
{
    var hash = BCrypt.Net.BCrypt.HashPassword(kvp.Key, 11);
    Console.WriteLine($"{kvp.Value}: {kvp.Key}");
    Console.WriteLine($"Hash: {hash}\n");
}

