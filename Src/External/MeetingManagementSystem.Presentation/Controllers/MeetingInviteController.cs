using MediatR;
using MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Commands;
using MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Queries;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class MeetingInviteController : ApiController
    {
        public MeetingInviteController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateInviteLink(CreateMeetingInviteCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateToken(ValidateInviteTokenQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
