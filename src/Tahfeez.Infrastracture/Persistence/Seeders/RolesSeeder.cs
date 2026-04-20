using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Tahfeez.Domain.Entities.Roles;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Infrastracture.Persistence.Seeders;

public class RolesSeeder
{
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<RolesSeeder> _logger;

    public RolesSeeder(RoleManager<Role> roleManager, ILogger<RolesSeeder> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        var roles = Enum.GetNames<UserRole>();

        foreach (var roleName in roles)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                _logger.LogInformation("Role '{Role}' already exists. Skipping.", roleName);
                continue;
            }

            var role = new Role
            {
                Name = roleName,
                NormalizedName = roleName.ToUpperInvariant(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                _logger.LogInformation("Role '{Role}' seeded successfully.", roleName);
            else
                _logger.LogError("Failed to seed role '{Role}': {Errors}", roleName,
                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
