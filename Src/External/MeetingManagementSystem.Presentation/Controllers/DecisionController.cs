using MediatR;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.CreateDecisionCommand;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.DeleteDecisionCommand;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.UpdateDecisionCommand;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionByIdQuery;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionsByMeetingIdQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class DecisionController : ApiController
    {
        public DecisionController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateDecision (CreateDecisionCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteDecision(DeleteDecisionCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateDecision(UpdateDecisionCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetDecisionByMeetingId(GetDecisionsByMeetingIdQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetDecisionById(GetDecisionByIdQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
