using MediatR;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.CreateMeetingRoleCommand;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.DeleteMeetingRoleCommand;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetAllRolesQuery;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByIdRolesQuery;
using MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByNameRolesQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRolesController : ApiController
    {
        public MeetingRolesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("CreateMeetingRole")]
        public async Task<IActionResult> CreateMeetingRole([FromBody] CreateMeetingRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("DeleteMeetingRole")]
        public async Task<IActionResult> DeleteMeetingRole([FromBody] DeleteMeetingRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles([FromBody] GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetByIdRoles")]
        public async Task<IActionResult> GetByIdRoles([FromBody] GetByIdRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetByNameRoles")]
        public async Task<IActionResult> GetByNameRoles([FromBody] GetByNameRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
