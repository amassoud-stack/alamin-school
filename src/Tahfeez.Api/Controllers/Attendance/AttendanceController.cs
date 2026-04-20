using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.Attendance.Commands.Create;
using Tahfeez.Application.Features.Attendance.Commands.Update;
using Tahfeez.Application.Features.Attendance.DTOs.Create;
using Tahfeez.Application.Features.Attendance.DTOs.Update;
using Tahfeez.Application.Features.Attendance.Queries.GetAttendanceByDate;
using Tahfeez.Application.Features.Attendance.Queries.GetAttendanceByUser;
using Tahfeez.Application.Features.Attendance.Queries.GetAttendanceReport;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Api.Controllers.Attendance;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IMediator _mediator;
    public AttendanceController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/attendance/date/{date} — Teacher, Assistant, Supervisor, Admin</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor},{Roles.Teacher},{Roles.Assistant}")]
    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetByDate([FromRoute] DateOnly date)
    {
        var result = await _mediator.Send(new GetAttendanceByDateQuery(date));
        return Ok(result);
    }

    /// <summary>GET /api/attendance/user/{userId} — Teacher, Supervisor, Admin, Parent, Student</summary>
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser([FromRoute] Guid userId)
    {
        var result = await _mediator.Send(new GetAttendanceByUserQuery(userId));
        return Ok(result);
    }

    /// <summary>GET /api/attendance/report?classId=&from=&to= — Supervisor, Admin</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor}")]
    [HttpGet("report")]
    public async Task<IActionResult> GetReport(
        [FromQuery] Guid classId,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var result = await _mediator.Send(new GetAttendanceReportQuery(classId, from, to));
        return Ok(result);
    }

    /// <summary>POST /api/attendance — Teacher, Assistant</summary>
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Assistant}")]
    [HttpPost]
    public async Task<IActionResult> Record([FromBody] RecordAttendanceDto command)
    {
        var result = await _mediator.Send(new RecordAttendanceCommand(command));
        if (result.IsFailure) return BadRequest(result);
        return Ok(result);
    }

    /// <summary>PUT /api/attendance/{id} — Teacher, Assistant</summary>
    [Authorize(Roles = $"{Roles.Teacher},{Roles.Assistant}")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAttendanceDto body)
    {
        var result = await _mediator.Send(new UpdateAttendanceCommand(id, body));
        if (result.IsFailure) return NotFound(result);
        return Ok(result);
    }
}
