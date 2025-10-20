using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using static MeetingManagementSystem.Domain.Enums.DecisionStatusEnum;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand
{
    public class CreateDecisionAssignmentCommand : IRequest<MessageResponse>
    {
        public string DecisionId { get; set; }
        public string UserId { get; set; }
        public DecisionStatus DecisionStatus { get; set; }
    }
}
