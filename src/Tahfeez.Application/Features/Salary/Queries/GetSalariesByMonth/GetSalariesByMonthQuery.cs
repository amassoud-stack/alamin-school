using MediatR;
using Tahfeez.Application.Features.Salary.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Queries.GetSalariesByMonth;

public record GetSalariesByMonthQuery(DateOnly Month) : IRequest<Result<IEnumerable<SalaryDto>>>;
