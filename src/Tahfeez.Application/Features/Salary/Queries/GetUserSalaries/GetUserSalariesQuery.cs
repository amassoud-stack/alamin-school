using MediatR;
using Tahfeez.Application.Features.Salary.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Queries.GetUserSalaries;

public record GetUserSalariesQuery(Guid UserId) : IRequest<Result<IEnumerable<SalaryDto>>>;
