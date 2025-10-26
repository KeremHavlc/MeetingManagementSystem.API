using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.JoinMeetingFromInviteCommand
{
    public class JoinMeetingFromInviteCommand : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
        public string UserId { get; set; }
    }
}
