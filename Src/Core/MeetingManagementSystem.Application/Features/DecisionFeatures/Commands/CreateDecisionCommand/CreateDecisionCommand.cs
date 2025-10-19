using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.CreateDecisionCommand
{
    public class CreateDecisionCommand : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
