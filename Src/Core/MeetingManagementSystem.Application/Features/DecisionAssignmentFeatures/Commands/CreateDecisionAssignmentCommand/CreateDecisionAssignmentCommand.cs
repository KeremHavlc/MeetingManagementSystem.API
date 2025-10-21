using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Enums;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand
{
    public class CreateDecisionAssignmentCommand : IRequest<MessageResponse>
    {
        public string DecisionId { get; set; }
        public string UserId { get; set; }
        public DecisionStatusEnum DecisionStatus { get; set; }
    }
}
