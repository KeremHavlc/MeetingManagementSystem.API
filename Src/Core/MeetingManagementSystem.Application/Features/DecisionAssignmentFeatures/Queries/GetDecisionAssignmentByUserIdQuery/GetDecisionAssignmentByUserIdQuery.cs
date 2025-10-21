using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByUserIdQuery
{
    public class GetDecisionAssignmentByUserIdQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
