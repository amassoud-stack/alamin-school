using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.Salary.Commands.CreateSalary;
using Tahfeez.Application.Features.Salary.Commands.MarkSalaryPaid;
using Tahfeez.Application.Features.Salary.Queries.GetSalariesByMonth;
using Tahfeez.Application.Features.Salary.Queries.GetUserSalaries;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Api.Controllers.Salary;

[Authorize(Roles = $"{Roles.Admin},{Roles.Accountant}")]
[ApiController]
[Route("api/[controller]")]
public class SalariesController : ControllerBase
{
    private readonly IMediator _mediator;
    public SalariesController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/salaries</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSalaryCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure) return BadRequest(result);
        return Ok(result);
    }

    /// <summary>POST /api/salaries/{id}/mark-paid</summary>
    [HttpPost("{id:guid}/mark-paid")]
    public async Task<IActionResult> MarkPaid([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new MarkSalaryPaidCommand(id));
        if (result.IsFailure) return NotFound(result);
        return Ok(result);
    }

    /// <summary>GET /api/salaries/month?month=2025-04-01</summary>
    [HttpGet("month")]
    public async Task<IActionResult> GetByMonth([FromQuery] DateOnly month)
    {
        var result = await _mediator.Send(new GetSalariesByMonthQuery(month));
        return Ok(result);
    }

    /// <summary>GET /api/salaries/user/{userId}</summary>
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser([FromRoute] Guid userId)
    {
        var result = await _mediator.Send(new GetUserSalariesQuery(userId));
        return Ok(result);
    }
}
