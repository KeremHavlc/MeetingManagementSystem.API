using MediatR;
using MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetDashboardStatsQuery;
using MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetMeetingStatisticsQuery;
using MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetUpcomingMeetingsQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class DashboardController : ApiController
    {
        public DashboardController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetDashboardStats(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetUpcomingMeetings([FromBody] GetUpcomingMeetingsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetMeetingStatistics([FromBody] GetMeetingStatisticsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
