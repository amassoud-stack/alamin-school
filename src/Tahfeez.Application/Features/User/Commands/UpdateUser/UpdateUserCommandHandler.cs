using FluentValidation;
using Mapster;
using Mapster.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateUserDto> _validator;
    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<UpdateUserDto> validator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request.userDto, cancellationToken);
        if (!validation.IsValid) return Result.Failure("validation error", validation.Errors.Select(e => e.ErrorMessage));

        var user = await _userRepository.GetByIdAsync(request.id, cancellationToken);
        if (user is null)
            return Result.Failure($"User with id '{request.id}' was not found.");

        user.Adapt<UpdateUserDto>();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success<UserDto>(user.Adapt<UserDto>());
    }
}
