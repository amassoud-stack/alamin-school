using MediatR;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.User.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<Result>;
