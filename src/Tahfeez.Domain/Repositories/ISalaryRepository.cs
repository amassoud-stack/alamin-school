using Tahfeez.Domain.Entities.Salaries;

namespace Tahfeez.Domain.Repositories;

public interface ISalaryRepository
{
    Task<Salary?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Salary>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Salary>> GetByMonthAsync(DateOnly month, CancellationToken cancellationToken = default);
    Task AddAsync(Salary salary, CancellationToken cancellationToken = default);
    Task UpdateAsync(Salary salary, CancellationToken cancellationToken = default);
}
