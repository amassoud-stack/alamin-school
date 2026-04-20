using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Tahfeez.Infrastracture.Persistence;
using Tahfeez.Infrastracture.Persistence.Interceptors;

namespace Tahfeez.Api.Extentions;

public static class DataBaseConfigExtentions
{
    public static void AddDatabaseConfig(this WebApplicationBuilder builder, string? connectionString)
    {
        builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            options.UseOpenIddict();

            // Inject the scoped audit interceptor
            var interceptor = serviceProvider.GetRequiredService<AuditInterceptor>();
            options.AddInterceptors(interceptor);
        });
    }
}
