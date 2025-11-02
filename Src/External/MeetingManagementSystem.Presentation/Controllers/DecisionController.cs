using MediatR;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.CreateDecisionCommand;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.DeleteDecisionCommand;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.UpdateDecisionCommand;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionByIdQuery;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionsByMeetingIdQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DecisionController : ApiController
    {
        public DecisionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("CreateDecision")]
        public async Task<IActionResult> CreateDecision([FromBody] CreateDecisionCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("DeleteDecision")]
        public async Task<IActionResult> DeleteDecision([FromBody] DeleteDecisionCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("UpdateDecision")]
        public async Task<IActionResult> UpdateDecision([FromBody] UpdateDecisionCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetDecisionByMeetingId")]
        public async Task<IActionResult> GetDecisionByMeetingId([FromBody] GetDecisionsByMeetingIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetDecisionById")]
        public async Task<IActionResult> GetDecisionById([FromBody] GetDecisionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
