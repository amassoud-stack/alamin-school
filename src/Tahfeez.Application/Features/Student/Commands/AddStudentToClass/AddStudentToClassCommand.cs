using MediatR;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Student.Commands.AddStudentToClass;

public record AddStudentToClassCommand(Guid StudentId, Guid ClassId, string Level) : IRequest<Result>;
