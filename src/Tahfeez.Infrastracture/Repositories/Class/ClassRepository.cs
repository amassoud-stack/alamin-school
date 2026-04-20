using Microsoft.EntityFrameworkCore;
using Tahfeez.Domain.Entities.Classes;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;

namespace Tahfeez.Infrastracture.Repositories.Class;

public class ClassRepository : IClassRepository
{
    private readonly AppDbContext _context;

    public ClassRepository(AppDbContext context) => _context = context;

    public async Task<Domain.Entities.Classes.Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Classes
            .Include(c => c.Teacher)
            .Include(c => c.Assistant)
            .Include(c => c.Supervisor)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IEnumerable<Domain.Entities.Classes.Class>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Classes
            .Include(c => c.Teacher)
            .Include(c => c.Assistant)
            .Include(c => c.Supervisor)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Domain.Entities.Classes.Class @class, CancellationToken cancellationToken = default)
        => await _context.Classes.AddAsync(@class, cancellationToken);

    public Task UpdateAsync(Domain.Entities.Classes.Class @class, CancellationToken cancellationToken = default)
    {
        _context.Classes.Update(@class);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Classes.FindAsync([id], cancellationToken);
        if (entity is not null) _context.Classes.Remove(entity);
    }
}
