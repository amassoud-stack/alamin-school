using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.Subscription.Commands.CreateSubscription;
using Tahfeez.Application.Features.Subscription.Queries.GetOverdueSubscriptions;
using Tahfeez.Application.Features.Subscription.Queries.GetStudentSubscriptions;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Api.Controllers.Subscription;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SubscriptionsController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/subscriptions — Accountant, Admin</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Accountant}")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure) return BadRequest(result);
        return Ok(result);
    }

    /// <summary>GET /api/subscriptions/student/{studentId}</summary>
    [HttpGet("student/{studentId:guid}")]
    public async Task<IActionResult> GetByStudent([FromRoute] Guid studentId)
    {
        var result = await _mediator.Send(new GetStudentSubscriptionsQuery(studentId));
        return Ok(result);
    }

    /// <summary>GET /api/subscriptions/overdue?daysOverdue=30 — Accountant, Admin</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Accountant}")]
    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue([FromQuery] int daysOverdue = 30)
    {
        var result = await _mediator.Send(new GetOverdueSubscriptionsQuery(daysOverdue));
        return Ok(result);
    }
}
