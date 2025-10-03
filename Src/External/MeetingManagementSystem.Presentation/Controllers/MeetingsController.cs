using MediatR;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.CreateMeetingCommand;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingByIdQuery;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingsByUserIdQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class MeetingsController : ApiController
    {
        public MeetingsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMeeting(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetMeetingById(GetMeetingByIdQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetMeetingByUserId (GetMeetingsByUserIdQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
