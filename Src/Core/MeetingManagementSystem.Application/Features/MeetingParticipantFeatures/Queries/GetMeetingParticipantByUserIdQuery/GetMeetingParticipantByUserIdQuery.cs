using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByUserIdQuery
{
    public class GetMeetingParticipantByUserIdQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
