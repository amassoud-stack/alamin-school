using MediatR;
using Microsoft.AspNetCore.Identity;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;
using AppUser = Tahfeez.Domain.Entities.Users.User;

namespace Tahfeez.Application.Features.Student.Commands.ActivateUser;

public class ActivateUserHandler : IRequestHandler<ActivateUserCommand, Result>
{
    private readonly UserManager<AppUser> _userManager;

    public ActivateUserHandler(UserManager<AppUser> userManager) => _userManager = userManager;

    public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) return Result.Failure("User not found.");

        user.Status = UserStatus.Active;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded
            ? Result.Success()
            : Result.Failure("Failed to activate user.", result.Errors.Select(e => e.Description));
    }
}
