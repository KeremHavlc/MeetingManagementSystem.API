using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.DeleteDecisionAssignmentCommand
{
    public class DeleteDecisionAssignmentCommand : IRequest<MessageResponse>
    {
        public string DecisionAssignmentId { get; set; }
    }
}
