using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionByIdQuery
{
    public class GetDecisionByIdQuery : IRequest<MessageResponse>
    {
        public string DecisionId { get; set; }
    }
}
