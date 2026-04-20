using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AppUser = Tahfeez.Domain.Entities.Users.User;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(UserManager<AppUser> userManager, ILogger<LoginCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Result.Failure<string>("Invalid email or password.");

            if (await _userManager.IsLockedOutAsync(user))
            {
                _logger.LogWarning("Login attempt for locked-out user {Email}.", request.Email);
                return Result.Failure<string>("Account is locked. Please try again later.");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                await _userManager.AccessFailedAsync(user);
                _logger.LogWarning("Invalid password attempt for user {Email}.", request.Email);
                return Result.Failure<string>("Invalid email or password.");
            }

            await _userManager.ResetAccessFailedCountAsync(user);
            return Result.Success(user.Id.ToString());
        }
    }
}