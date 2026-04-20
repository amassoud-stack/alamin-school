using MediatR;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Student.Commands.ActivateUser;

public record ActivateUserCommand(Guid UserId) : IRequest<Result>;
