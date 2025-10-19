using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionsByMeetingIdQuery
{
    public class GetDecisionsByMeetingIdQuery : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
    }
}
