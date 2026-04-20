using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Queries.GetAllClasses;

public record GetAllClassesQuery : IRequest<Result<IEnumerable<ClassListItemDto>>>;