using MediatR;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.DeleteDecisionAssignmentCommand;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.UpdateDecisionAssignmentStatusCommand;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByDecisionIdQuery;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByUserIdQuery;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetMeetingDecisionProgressQuery;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateDecisionAssignmentStatus(UpdateDecisionAssignmentStatusCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetDecisionAssignmentByDecisionId(GetDecisionAssignmentByDecisionIdQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetDecisionAssignmentByUserId(GetDecisionAssignmentByUserIdQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetMeetingDecisionProgress(GetMeetingDecisionProgressQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
