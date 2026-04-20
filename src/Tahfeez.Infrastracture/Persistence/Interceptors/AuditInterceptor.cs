using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tahfeez.SharedKernal.Interfaces;

namespace Tahfeez.Infrastracture.Persistence.Interceptors;

/// <summary>
/// Automatically fills CreatedAt/UpdatedAt/CreatedBy/UpdatedBy/DeletedAt
/// on any entity that implements IFullAudit or ISoftDeletable.
/// </summary>
public sealed class AuditInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, InterceptionResult<int> result)
    {
        Audit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        Audit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private void Audit(DbContext? context)
    {
        if (context is null) return;

        var currentUser = _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";

        var now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is IFullAudit audit)
            {
                if (entry.State == EntityState.Added)
                {
                    audit.CreatedAt = now;
                    audit.CreatedBy = currentUser;
                }

                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    audit.UpdatedAt = now;
                    audit.UpdatedBy = currentUser;
                }
            }

            if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeletable soft)
            {
                entry.State = EntityState.Modified;
                soft.IsDeleted = true;

                // DeletedAt lives on IFullAudit (which most entities also implement)
                if (soft is IFullAudit fullAudit)
                    fullAudit.DeletedAt = now;
            }
        }
    }
}
