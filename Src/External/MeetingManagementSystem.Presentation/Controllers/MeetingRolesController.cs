using MediatR;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.CreateMeetingRoleCommand;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.DeleteMeetingRoleCommand;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetAllRolesQuery;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByIdRolesQuery;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByNameRolesQuery;
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
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteMeetingRole(DeleteMeetingRoleCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllRoles(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetByIdRoles(GetByIdRolesQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetByNameRoles(GetByNameRolesQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
