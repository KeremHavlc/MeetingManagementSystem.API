using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.DeleteMeetingCommand
{
    public class DeleteMeetingCommand : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
    }
}
