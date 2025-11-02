using MediatR;
using MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Commands;
using MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Queries;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingInviteController : ApiController
    {
        public MeetingInviteController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("CreateInviteLink")]
        public async Task<IActionResult> CreateInviteLink([FromBody] CreateMeetingInviteCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("ValidateToken")]
        public async Task<IActionResult> ValidateToken([FromBody] ValidateInviteTokenQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
