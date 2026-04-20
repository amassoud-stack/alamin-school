using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Auth.Commands.Login
{
    public record LoginCommand
     (
        string Email,
        string Password
     ): IRequest<Result<string>>;
}
