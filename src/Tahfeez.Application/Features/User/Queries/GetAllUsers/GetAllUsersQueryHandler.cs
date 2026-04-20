using MediatR;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserListItemDto>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserListItemDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(isDeleted:false, cancellationToken);

        var dtos = users.Select(u => new UserListItemDto(u.Id, u.FullName, u.Email, u.CreatedAt, u.UpdatedAt, u.Status));
        return Result.Success(dtos);
    }
}
