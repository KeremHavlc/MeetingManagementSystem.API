using MediatR;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.DeleteDecisionAssignmentCommand;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.UpdateDecisionAssignmentStatusCommand;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByDecisionIdQuery;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByUserIdQuery;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDetailsDecisionByUserIdQuery;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetMeetingDecisionProgressQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DecisionAssignmentController : ApiController
    {
        public DecisionAssignmentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("CreateDecisionAssignment")]
        public async Task<IActionResult> CreateDecisionAssignment([FromBody] CreateDecisionAssignmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("DeleteDecisionAssignment")]
        public async Task<IActionResult> DeleteDecisionAssignment([FromBody] DeleteDecisionAssignmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("UpdateDecisionAssignmentStatus")]
        public async Task<IActionResult> UpdateDecisionAssignmentStatus([FromBody] UpdateDecisionAssignmentStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetDecisionAssignmentByDecisionId")]
        public async Task<IActionResult> GetDecisionAssignmentByDecisionId([FromBody] GetDecisionAssignmentByDecisionIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetDecisionAssignmentByUserId")]
        public async Task<IActionResult> GetDecisionAssignmentByUserId([FromBody] GetDecisionAssignmentByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetMeetingDecisionProgress")]
        public async Task<IActionResult> GetMeetingDecisionProgress([FromBody] GetMeetingDecisionProgressQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetDetailsDecisionByUserId")]
        public async Task<IActionResult> GetDetailsDecisionByUserId([FromBody] GetDetailsDecisionByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
