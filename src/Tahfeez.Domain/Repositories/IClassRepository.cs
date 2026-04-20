using Tahfeez.Domain.Entities.Classes;

namespace Tahfeez.Domain.Repositories;

public interface IClassRepository
{
    Task<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Class @class, CancellationToken cancellationToken = default);
    Task UpdateAsync(Class @class, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
