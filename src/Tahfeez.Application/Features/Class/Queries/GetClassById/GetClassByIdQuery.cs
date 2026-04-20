using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Queries.GetClassById;

public record GetClassByIdQuery(Guid ClassId) : IRequest<Result<ClassDto>>;