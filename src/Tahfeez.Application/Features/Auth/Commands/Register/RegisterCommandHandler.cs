using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Tahfeez.SharedKernal.Common;
using AppUser = Tahfeez.Domain.Entities.Users.User;

namespace Tahfeez.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IValidator<RegisterCommand> _validator;

        public RegisterCommandHandler(UserManager<AppUser> userManager, ILogger<RegisterCommandHandler> logger, IValidator<RegisterCommand> validator)
        {
            _userManager = userManager;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken = default)
        {
            var valid = await _validator.ValidateAsync(request);
            if (!valid.IsValid) return Result.Failure("validation error",valid.Errors.Select(e => e.ErrorMessage));

            var existing = await _userManager.FindByEmailAsync(request.Email);
            if (existing is not null)
                return Result.Failure("Email is already registered.");

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                _logger.LogError("Failed to register user {Email}: {Errors}", request.Email, errors);
                return Result.Failure(errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, request.Role.ToString());
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                _logger.LogError("Role assignment failed for {Email}: {Errors}", request.Email, errors);
                return Result.Failure(errors);
            }

            _logger.LogInformation("User {Email} registered successfully with role {Role}.", request.Email, request.Role);
            return Result.Success();
        }
    }
}
