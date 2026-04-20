using MediatR;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.UpdateClass;

public record UpdateClassCommand(Guid ClassId, UpdateClassDto Dto) : IRequest<Result>;