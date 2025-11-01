using MediatR;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.AddMeetingParticipantCommand;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.DeleteMeetingParticipantCommand;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.JoinMeetingFromInviteCommand;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByMeetingIdQuery;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByUserIdQuery;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantsQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingParticipantController : ApiController
    {
        public MeetingParticipantController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("AddMeetingParticipant")]
        public async Task<IActionResult> AddMeetingParticipant([FromBody] AddMeetingParticipantCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("DeleteMeetingParticipant")]
        public async Task<IActionResult> DeleteMeetingParticipant([FromBody] DeleteMeetingParticipantCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetMeetingParticipantsWithUsers")]
        public async Task<IActionResult> GetMeetingParticipantsWithUsersAsync([FromBody] GetMeetingParticipantsQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetMeetingParticipantsByUserId")]
        public async Task<IActionResult> GetMeetingParticipantsByUserId([FromBody] GetMeetingParticipantByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("JoinFromInvite")]
        public async Task<IActionResult> JoinFromInvite([FromBody] JoinMeetingFromInviteCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetMeetingParticipantByMeetingId")]
        public async Task<IActionResult> GetMeetingParticipantByMeetingId([FromBody] GetMeetingParticipantByMeetingIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
