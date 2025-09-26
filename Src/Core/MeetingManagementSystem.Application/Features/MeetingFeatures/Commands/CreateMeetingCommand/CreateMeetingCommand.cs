using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.CreateMeetingCommand
{
    public class CreateMeetingCommand : IRequest<MessageResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledAt { get; set; }
        public Guid CreatedByUserId { get; set; }

    }
}
