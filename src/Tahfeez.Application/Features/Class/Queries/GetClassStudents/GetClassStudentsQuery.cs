using MediatR;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Queries.GetClassStudents;

public record GetClassStudentsQuery(Guid ClassId) : IRequest<Result<IEnumerable<UserListItemDto>>>;