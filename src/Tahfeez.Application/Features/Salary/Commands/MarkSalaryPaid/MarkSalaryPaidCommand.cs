using MediatR;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Commands.MarkSalaryPaid;

public record MarkSalaryPaidCommand(Guid SalaryId) : IRequest<Result>;
