using MediatR;

namespace Tahfeez.Application.Features.Auth.Commands.Connect
{
    public class ConnectCommandHandler : IRequestHandler<ConnectCommand ,string>
    {
        Task<string> IRequestHandler<ConnectCommand, string>.Handle(ConnectCommand request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
