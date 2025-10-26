using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByMeetingIdQuery
{
    public class GetMeetingParticipantByMeetingIdQuery : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
    }
}
