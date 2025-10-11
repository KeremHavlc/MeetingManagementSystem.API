using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.AddMeetingParticipantCommand
{
    public class AddMeetingParticipantCommand : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
        public string UserId { get; set; }
    }
}
