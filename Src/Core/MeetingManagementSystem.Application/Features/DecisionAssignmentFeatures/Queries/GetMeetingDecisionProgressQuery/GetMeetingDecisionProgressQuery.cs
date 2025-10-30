using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetMeetingDecisionProgressQuery
{
    public class GetMeetingDecisionProgressQuery : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
    }
}
