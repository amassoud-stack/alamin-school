using Microsoft.EntityFrameworkCore;
using Tahfeez.Domain.Entities.Subscriptions;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;

namespace Tahfeez.Infrastracture.Repositories.Subscription;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _context;

    public SubscriptionRepository(AppDbContext context) => _context = context;

    public async Task<Domain.Entities.Subscriptions.Subscription?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
        => await _context.Subscriptions.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<Domain.Entities.Subscriptions.Subscription>> GetByStudentAsync(
        Guid studentId, CancellationToken cancellationToken = default)
        => await _context.Subscriptions
            .Where(s => s.StudentId == studentId)
            .OrderByDescending(s => s.PaymentDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Domain.Entities.Subscriptions.Subscription>> GetOverdueAsync(
        int daysOverdue, CancellationToken cancellationToken = default)
    {
        var cutoff = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-daysOverdue));
        return await _context.Subscriptions
            .Include(s => s.Student)
            .Where(s => s.PaymentDate < cutoff)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Domain.Entities.Subscriptions.Subscription subscription,
        CancellationToken cancellationToken = default)
        => await _context.Subscriptions.AddAsync(subscription, cancellationToken);
}
