using Microsoft.EntityFrameworkCore;
using Tahfeez.Domain.Entities.Atendences;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;

namespace Tahfeez.Infrastracture.Repositories.Attendance;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _context;

    public AttendanceRepository(AppDbContext context) => _context = context;

    public async Task<Atendence?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Attendances.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<Atendence>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Attendances
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.Date)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Atendence>> GetByDateAsync(DateOnly date, CancellationToken cancellationToken = default)
        => await _context.Attendances
            .Include(a => a.User)
            .Where(a => a.Date == date)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Atendence>> GetByClassAndDateRangeAsync(
        Guid classId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default)
        => await _context.Attendances
            .Include(a => a.User)
            .Where(a => a.User.ClassId == classId && a.Date >= from && a.Date <= to)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Atendence attendance, CancellationToken cancellationToken = default)
        => await _context.Attendances.AddAsync(attendance, cancellationToken);

    public Task UpdateAsync(Atendence attendance, CancellationToken cancellationToken = default)
    {
        _context.Attendances.Update(attendance);
        return Task.CompletedTask;
    }
}
