using MediatR;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.AssignStaff;

public record AssignStaffCommand(Guid ClassId, AssignStaffDto Dto) : IRequest<Result>;