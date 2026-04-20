using Microsoft.EntityFrameworkCore;
using Tahfeez.Domain.Entities.Recitations;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;

namespace Tahfeez.Infrastracture.Repositories.Recitation;

public class RecitationRepository : IRecitationRepository
{
    private readonly AppDbContext _context;

    public RecitationRepository(AppDbContext context) => _context = context;

    public async Task<Domain.Entities.Recitations.Recitation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Recitations.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<Domain.Entities.Recitations.Recitation>> GetByStudentAsync(
        Guid studentId, CancellationToken cancellationToken = default)
        => await _context.Recitations
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.Date)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Domain.Entities.Recitations.Recitation>> GetByClassAndMonthAsync(
        Guid classId, DateOnly month, CancellationToken cancellationToken = default)
        => await _context.Recitations
            .Include(r => r.Student)
            .Where(r => r.Student.ClassId == classId
                && r.Date.Year == month.Year
                && r.Date.Month == month.Month)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<int> GetMonthlyTotalScoreAsync(
        Guid studentId, DateOnly month, CancellationToken cancellationToken = default)
        => await _context.Recitations
            .Where(r => r.StudentId == studentId
                && r.Date.Year == month.Year
                && r.Date.Month == month.Month)
            .SumAsync(r => r.Grade, cancellationToken);

    public async Task AddAsync(Domain.Entities.Recitations.Recitation recitation, CancellationToken cancellationToken = default)
        => await _context.Recitations.AddAsync(recitation, cancellationToken);
}
