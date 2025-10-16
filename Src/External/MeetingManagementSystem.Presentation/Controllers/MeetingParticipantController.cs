using MediatR;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.DeleteMeetingCommand;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingByIdQuery;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.AddMeetingParticipantCommand;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.DeleteMeetingParticipantCommand;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantsQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    public class MeetingParticipantController : ApiController
    {
        public MeetingParticipantController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMeetingParticipant(AddMeetingParticipantCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteMeetingParticipant(DeleteMeetingParticipantCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetMeetingParticipantsWithUsersAsync(GetMeetingParticipantsQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
