using Microsoft.EntityFrameworkCore;
using Tahfeez.Domain.Entities.Salaries;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;

namespace Tahfeez.Infrastracture.Repositories.Salary;

public class SalaryRepository : ISalaryRepository
{
    private readonly AppDbContext _context;

    public SalaryRepository(AppDbContext context) => _context = context;

    public async Task<Domain.Entities.Salaries.Salary?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
        => await _context.Salaries.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<Domain.Entities.Salaries.Salary>> GetByUserAsync(
        Guid userId, CancellationToken cancellationToken = default)
        => await _context.Salaries
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.SalaryMonth)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Domain.Entities.Salaries.Salary>> GetByMonthAsync(
        DateOnly month, CancellationToken cancellationToken = default)
        => await _context.Salaries
            .Include(s => s.User)
            .Where(s => s.SalaryMonth == month)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Domain.Entities.Salaries.Salary salary,
        CancellationToken cancellationToken = default)
        => await _context.Salaries.AddAsync(salary, cancellationToken);

    public Task UpdateAsync(Domain.Entities.Salaries.Salary salary,
        CancellationToken cancellationToken = default)
    {
        _context.Salaries.Update(salary);
        return Task.CompletedTask;
    }
}
