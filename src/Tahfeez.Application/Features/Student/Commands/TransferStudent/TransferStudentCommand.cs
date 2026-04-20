using MediatR;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Student.Commands.TransferStudent;

public record TransferStudentCommand(Guid StudentId, Guid NewClassId) : IRequest<Result>;
