using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingsByUserIdQuery
{
    public class GetMeetingsByUserIdQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
