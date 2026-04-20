using MediatR;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.User.Commands.CreateUser;

public record CreateUserCommand(CreateUserDto userDto) : IRequest<Result<Guid>>;