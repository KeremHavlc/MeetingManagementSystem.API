using MediatR;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.DeleteDecisionAssignmentCommand;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class DecisionAssignmentController : ApiController
    {
        public DecisionAssignmentController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateDecisionAssignment(CreateDecisionAssignmentCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteDecisionAssignment(DeleteDecisionAssignmentCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
