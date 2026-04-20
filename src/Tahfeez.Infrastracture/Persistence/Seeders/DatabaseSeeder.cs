using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tahfeez.Infrastracture.Persistence.Seeders;

public class DatabaseSeeder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(IServiceProvider serviceProvider, ILogger<DatabaseSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Starting database seeding...");

        await using var scope = _serviceProvider.CreateAsyncScope();
        var sp = scope.ServiceProvider;

        // Order matters: Roles → Users → OpenIddict
        await sp.GetRequiredService<RolesSeeder>().SeedAsync();
        await sp.GetRequiredService<UsersSeeder>().SeedAsync();
        await sp.GetRequiredService<OpeniddictSeeder>().SeedAsync();

        _logger.LogInformation("Database seeding completed.");
    }
}
