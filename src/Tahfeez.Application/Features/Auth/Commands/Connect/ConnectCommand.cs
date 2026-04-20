using MediatR;

namespace Tahfeez.Application.Features.Auth.Commands.Connect
{
    public record ConnectCommand
    (
    ) : IRequest<string>;
}
