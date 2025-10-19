using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.DeleteDecisionCommand
{
    public class DeleteDecisionCommand : IRequest<MessageResponse>
    {
        public string DecisionId { get; set; }
    }
}
