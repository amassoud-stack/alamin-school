using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Infrastracture.Persistence.Seeders;

public class UsersSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UsersSeeder> _logger;

    public UsersSeeder(UserManager<User> userManager, ILogger<UsersSeeder> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        await SeedAdminAsync();
    }

    // ---------------------------------------------------------------
    private async Task SeedAdminAsync()
    {
        const string adminEmail = "admin@tahfeez.com";
        const string adminPassword = "Admin@123456";
        const string adminFullName = "Admin User";

        var existing = await _userManager.FindByEmailAsync(adminEmail);
        if (existing is not null)
        {
            _logger.LogInformation("Admin user already exists. Skipping.");
            return;
        }

        var admin = new User
        {
            Id = Guid.NewGuid(),
            FullName = adminFullName,
            UserName = adminEmail,
            NormalizedUserName = adminEmail.ToUpperInvariant(),
            Email = adminEmail,
            NormalizedEmail = adminEmail.ToUpperInvariant(),
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System"
        };

        var createResult = await _userManager.CreateAsync(admin, adminPassword);
        if (!createResult.Succeeded)
        {
            _logger.LogError("Failed to create admin user: {Errors}",
                string.Join(", ", createResult.Errors.Select(e => e.Description)));
            return;
        }

        var roleResult = await _userManager.AddToRoleAsync(admin, nameof(UserRole.Admin));
        if (roleResult.Succeeded)
            _logger.LogInformation("Admin user seeded successfully.");
        else
            _logger.LogError("Admin user created but role assignment failed: {Errors}",
                string.Join(", ", roleResult.Errors.Select(e => e.Description)));
    }
}
