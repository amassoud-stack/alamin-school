using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.Application.Features.User.Validators.Create;
using Tahfeez.Application.Utilities;
using Tahfeez.Domain.Entities.Roles;
using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Domain.Entities.Users.User> _userManager;
    private readonly IValidator<CreateUserDto> _validator;
    public CreateUserCommandHandler(IUnitOfWork unitOfWork, UserManager<Domain.Entities.Users.User> userManager, IValidator<CreateUserDto> validator)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request.userDto, cancellationToken);
        if (!validator.IsValid) return Result.Failure<Guid>("Validation Error", validator.Errors.Select(e => e.ErrorMessage));
        
        var exists = await _userManager.FindByEmailAsync(request.userDto.Email) != null;
        if (exists)
            return Result.Failure<Guid>("A user with this email already exists.");

        var user = new Domain.Entities.Users.User
        {
            Email = request.userDto.Email,
            UserName = request.userDto.UserName,
            FullName = request.userDto.UserName, // caller can update profile later
            EmailConfirmed = true,
            Status = UserStatus.Pending          // all new users start Pending
        };

        var createResult = await _userManager.CreateAsync(user, request.userDto.Password);
        if( !createResult.Succeeded)
            return Result.Failure<Guid>("Failed to create user.", createResult.Errors.Select(e=>e.Description));

        await _userManager.AddToRoleAsync(user, RoleUtilities.GetRoleInString(request.userDto.Role));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}
