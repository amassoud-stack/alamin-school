using Hangfire.Dashboard;

namespace Tahfeez.Api.Middleware;

/// <summary>
/// Restricts the Hangfire dashboard to authenticated admin users in production.
/// In Development, the dashboard is open to all.
/// </summary>
public sealed class HangfireDashboardAuthFilter : IDashboardAuthorizationFilter
{
    private readonly IHostEnvironment _env;

    public HangfireDashboardAuthFilter(IHostEnvironment env)
    {
        _env = env;
    }

    public bool Authorize(DashboardContext context)
    {
        // Allow unrestricted access in Development
        if (_env.IsDevelopment()) return true;

        var httpContext = context.GetHttpContext();

        // Must be authenticated and in the Admin role
        return httpContext.User.Identity?.IsAuthenticated == true
            && httpContext.User.IsInRole(Tahfeez.SharedKernal.Coonstants.Roles.Admin);
    }
}
