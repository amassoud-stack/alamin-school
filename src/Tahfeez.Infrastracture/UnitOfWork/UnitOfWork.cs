using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;

namespace Tahfeez.Infrastracture;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync();

    public int SaveChanges(CancellationToken cancellationToken = default)
        => _context.SaveChanges();
}
