using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByDecisionIdQuery
{
    public class GetDecisionAssignmentByDecisionIdQuery : IRequest<MessageResponse>
    {
        public string DecisionId { get; set; }
    }
}
