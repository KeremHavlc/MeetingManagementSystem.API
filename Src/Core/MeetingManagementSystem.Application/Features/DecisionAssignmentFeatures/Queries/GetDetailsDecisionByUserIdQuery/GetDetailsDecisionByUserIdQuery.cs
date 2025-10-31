using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDetailsDecisionByUserIdQuery
{
    public class GetDetailsDecisionByUserIdQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
