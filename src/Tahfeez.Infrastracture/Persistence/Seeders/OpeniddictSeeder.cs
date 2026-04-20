using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Tahfeez.Infrastracture.Persistence.Seeders;

public class OpeniddictSeeder
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<OpeniddictSeeder> _logger;

    public OpeniddictSeeder(
        IOpenIddictApplicationManager applicationManager,
        IConfiguration configuration,
        ILogger<OpeniddictSeeder> logger)
    {
        _applicationManager = applicationManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        var clients = _configuration.GetSection("ApplicationClients").GetChildren();

        foreach (var client in clients)
        {
            var clientId = client["ClientId"];
            var clientSecret = client["ClientSecret"];
            var grantTypes = client.GetSection("AllowedGrantTypes")
                                   .Get<string[]>() ?? [];

            if (string.IsNullOrWhiteSpace(clientId))
                continue;

            if (await _applicationManager.FindByClientIdAsync(clientId) is not null)
            {
                _logger.LogInformation("OpenIddict application '{ClientId}' already exists. Skipping.", clientId);
                continue;
            }

            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                DisplayName = client.Key,
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.Endpoints.Introspection,
                    Permissions.Endpoints.Revocation,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                }
            };

            // Map grant type strings from config to OpenIddict permission constants
            foreach (var grant in grantTypes)
            {
                var permission = grant switch
                {
                    "authorization_code"   => Permissions.GrantTypes.AuthorizationCode,
                    "client_credentials"   => Permissions.GrantTypes.ClientCredentials,
                    "refresh_token"        => Permissions.GrantTypes.RefreshToken,
                    "password"             => Permissions.GrantTypes.Password,
                    _                      => null
                };

                if (permission is not null)
                    descriptor.Permissions.Add(permission);
            }

            await _applicationManager.CreateAsync(descriptor);
            _logger.LogInformation("OpenIddict application '{ClientId}' seeded successfully.", clientId);
        }
    }
}
