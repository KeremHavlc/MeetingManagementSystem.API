using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Enums;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.UpdateDecisionAssignmentStatusCommand
{
    public class UpdateDecisionAssignmentStatusCommand : IRequest<MessageResponse>
    {
        public string DecisionAssignmentId { get; set; }
        public DecisionStatusEnum DecisionStatusEnum { get; set; }
    }
}
