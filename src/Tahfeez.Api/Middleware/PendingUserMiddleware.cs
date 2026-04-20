using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tahfeez.Domain.Enums;
using AppUser = Tahfeez.Domain.Entities.Users.User;

namespace Tahfeez.Api.Middleware;

/// <summary>
/// Returns 403 Forbidden for any authenticated user whose Status is Pending.
/// Unauthenticated requests and the /connect/token endpoint pass through freely.
/// </summary>
public sealed class PendingUserMiddleware
{
    private readonly RequestDelegate _next;

    public PendingUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
    {
        // Allow unauthenticated requests and the token endpoint through
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            await _next(context);
            return;
        }

        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is not null)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is not null && user.Status == UserStatus.Pending)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "account_pending",
                    message = "Your account is pending activation by an administrator."
                });
                return;
            }
        }

        await _next(context);
    }
}
