using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.Recitation.Commands.LogRecitation;
using Tahfeez.Application.Features.Recitation.Queries.GetRecitationsByClass;
using Tahfeez.Application.Features.Recitation.Queries.GetRecitationsByStudent;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Api.Controllers.Recitation;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RecitationsController : ControllerBase
{
    private readonly IMediator _mediator;
    public RecitationsController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/recitations — Teacher, Assistant</summary>
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Assistant}")]
    [HttpPost]
    public async Task<IActionResult> Log([FromBody] LogRecitationCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure) return BadRequest(result);
        return Ok(result);
    }

    /// <summary>GET /api/recitations/student/{studentId} — Teacher, Student, Parent, Admin</summary>
    [HttpGet("student/{studentId:guid}")]
    public async Task<IActionResult> GetByStudent([FromRoute] Guid studentId)
    {
        var result = await _mediator.Send(new GetRecitationsByStudentQuery(studentId));
        return Ok(result);
    }

    /// <summary>GET /api/recitations/class/{classId}?month=2025-04 — Teacher, Supervisor, Admin</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor},{Roles.Teacher}")]
    [HttpGet("class/{classId:guid}")]
    public async Task<IActionResult> GetByClass(
        [FromRoute] Guid classId,
        [FromQuery] DateOnly month)
    {
        var result = await _mediator.Send(new GetRecitationsByClassQuery(classId, month));
        return Ok(result);
    }
}
