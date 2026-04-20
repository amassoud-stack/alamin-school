using MediatR;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.User.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<Result<IEnumerable<UserListItemDto>>>;
