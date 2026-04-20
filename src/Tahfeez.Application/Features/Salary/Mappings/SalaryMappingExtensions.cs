using Tahfeez.Application.Features.Salary.DTOs;

namespace Tahfeez.Application.Features.Salary.Mappings;

internal static class SalaryMappingExtensions
{
    internal static SalaryDto Map(this Domain.Entities.Salaries.Salary salary)
        => new(
            salary.Id,
            salary.UserId,
            salary.User?.FullName ?? string.Empty,
            salary.Role,
            salary.Amount,
            salary.Status,
            salary.SalaryMonth,
            salary.PaidAt,
            salary.Notes
        );
}
