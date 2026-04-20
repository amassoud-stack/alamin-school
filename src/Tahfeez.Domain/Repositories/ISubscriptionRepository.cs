using Tahfeez.Domain.Entities.Subscriptions;

namespace Tahfeez.Domain.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Subscription>> GetByStudentAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Subscription>> GetOverdueAsync(int daysOverdue, CancellationToken cancellationToken = default);
    Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default);
}
