using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.User.Commands.UpdateUser;

public record UpdateUserCommand(Guid id, UpdateUserDto userDto) : IRequest<Result>;