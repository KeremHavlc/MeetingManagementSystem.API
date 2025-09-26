using MediatR;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.CreateMeetingCommand;
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
    }
}
