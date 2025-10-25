using MediatR;
using MeetingManagementSystem.Application.Features.ChatFeatures.Queries.GetMessagesByMeetingIdQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    //[Authorize]
    public class ChatMessagesController : ApiController
    {
        public ChatMessagesController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet("by-meeting")]
        public async Task<IActionResult> GetByMeeting([FromQuery] Guid meetingId , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMessagesByMeetingIdQuery
            {
                MeetingId = meetingId
            },cancellationToken);
            return Ok(result);
        }
    }
}
