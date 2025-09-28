using MediatR;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.CreateMeetingRoleCommand;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class MeetingRolesController : ApiController
    {
        public MeetingRolesController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMeetingRole(CreateMeetingRoleCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
