using TaskFlow.Models;
using TaskFlow.Models.Enums;

namespace TaskFlow.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Users.Any()) return; 

        var adminUser = new User
        {
            Email = "admin@taskflow.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            FullName = "System Admin",
            Role = UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(adminUser);
        
        await context.SaveChangesAsync();
    }
}