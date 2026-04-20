using Microsoft.EntityFrameworkCore;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;
using AppUser = Tahfeez.Domain.Entities.Users.User;

namespace Tahfeez.Infrastracture.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<IEnumerable<AppUser>> GetAllAsync(bool isDeleted = false, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().Where(u => u.IsDeleted == isDeleted).ToListAsync(cancellationToken);

    public async Task AddAsync(AppUser user, CancellationToken cancellationToken = default)
        => await _context.Users.AddAsync(user, cancellationToken);
}
