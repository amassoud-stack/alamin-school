using MediatR;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.CreateClass;

public record CreateClassCommand(CreateClassDto Dto) : IRequest<Result<Guid>>;