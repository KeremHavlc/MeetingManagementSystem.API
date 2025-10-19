using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.UpdateDecisionCommand
{
    public class UpdateDecisionCommand : IRequest<MessageResponse>
    {
        public string DecisionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
