using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.DeleteMeetingParticipantCommand
{
    public class DeleteMeetingParticipantCommandHandler : IRequestHandler<DeleteMeetingParticipantCommand, MessageResponse>
    {
        public Task<MessageResponse> Handle(DeleteMeetingParticipantCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
