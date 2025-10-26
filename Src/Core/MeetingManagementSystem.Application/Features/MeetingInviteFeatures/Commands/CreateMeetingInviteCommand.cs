using MediatR;
using MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Dto;

namespace MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Commands
{
    public class CreateMeetingInviteCommand : IRequest<CreateMeetingInviteResponseDto>
    {
        public Guid MeetingId { get; set; }
    }
}
