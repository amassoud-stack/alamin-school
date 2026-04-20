using Tahfeez.Domain.Entities.Atendences;

namespace Tahfeez.Domain.Repositories;

public interface IAttendanceRepository
{
    Task<Atendence?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Atendence>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Atendence>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<Atendence>> GetByClassAndDateRangeAsync(Guid classId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default);
    Task AddAsync(Atendence attendance, CancellationToken cancellationToken = default);
    Task UpdateAsync(Atendence attendance, CancellationToken cancellationToken = default);
}
