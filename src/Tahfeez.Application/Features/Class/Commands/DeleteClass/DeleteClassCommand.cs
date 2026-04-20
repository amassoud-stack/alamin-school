using MediatR;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.DeleteClass;

public record DeleteClassCommand(Guid ClassId) : IRequest<Result>;