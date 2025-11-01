using MediatR;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.CreateMeetingCommand;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.DeleteMeetingCommand;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingByIdQuery;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingsByUserIdQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ApiController
    {
        public MeetingsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("CreateMeeting")]
        public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("DeleteMeeting")]
        public async Task<IActionResult> DeleteMeeting([FromBody] DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetMeetingById")]
        public async Task<IActionResult> GetMeetingById([FromBody] GetMeetingByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetMeetingByUserId")]
        public async Task<IActionResult> GetMeetingByUserId([FromBody] GetMeetingsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
