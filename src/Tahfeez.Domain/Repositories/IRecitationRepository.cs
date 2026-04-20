using Tahfeez.Domain.Entities.Recitations;

namespace Tahfeez.Domain.Repositories;

public interface IRecitationRepository
{
    Task<Recitation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Recitation>> GetByStudentAsync(Guid studentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Recitation>> GetByClassAndMonthAsync(Guid classId, DateOnly month, CancellationToken cancellationToken = default);
    Task<int> GetMonthlyTotalScoreAsync(Guid studentId, DateOnly month, CancellationToken cancellationToken = default);
    Task AddAsync(Recitation recitation, CancellationToken cancellationToken = default);
}
