using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantsQuery
{
    public class GetMeetingParticipantsQuery : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
    }
}
