using MediatR;
using MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetDashboardStatsQuery;
using MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetMeetingStatisticsQuery;
using MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetUpcomingMeetingsQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ApiController
    {
        public DashboardController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("GetDashboardStats")]
        public async Task<IActionResult> GetDashboardStats([FromBody] GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetUpcomingMeetings")]
        public async Task<IActionResult> GetUpcomingMeetings([FromBody] GetUpcomingMeetingsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetMeetingStatistics")]
        public async Task<IActionResult> GetMeetingStatistics([FromBody] GetMeetingStatisticsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
