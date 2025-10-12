using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.DeleteMeetingParticipantCommand
{
    public class DeleteMeetingParticipantCommand : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
        public string MeetingId { get; set; }
    }
}
