using MediatR;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Commands.CreateSalary;

public record CreateSalaryCommand(
    Guid UserId,
    UserRole Role,
    decimal Amount,
    DateOnly SalaryMonth,
    string? Notes
) : IRequest<Result<Guid>>;
