using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Salary.DTOs;

public record SalaryDto(
    Guid Id,
    Guid UserId,
    string UserName,
    UserRole Role,
    decimal Amount,
    SalaryStatus Status,
    DateOnly SalaryMonth,
    DateTime? PaidAt,
    string? Notes
);
