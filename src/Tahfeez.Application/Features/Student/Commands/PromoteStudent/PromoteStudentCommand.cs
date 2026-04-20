using MediatR;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Student.Commands.PromoteStudent;

public record PromoteStudentCommand(Guid StudentId, string NewLevel, Guid? NewClassId) : IRequest<Result>;
