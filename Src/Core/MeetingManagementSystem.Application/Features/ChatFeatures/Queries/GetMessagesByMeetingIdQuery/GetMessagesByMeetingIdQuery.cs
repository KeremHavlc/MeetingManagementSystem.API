using MediatR;
using MeetingManagementSystem.Application.Features.ChatFeatures.Dto;

namespace MeetingManagementSystem.Application.Features.ChatFeatures.Queries.GetMessagesByMeetingIdQuery
{
    public class GetMessagesByMeetingIdQuery : IRequest<IReadOnlyList<ChatMessageDto>>
    {
        public Guid MeetingId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
