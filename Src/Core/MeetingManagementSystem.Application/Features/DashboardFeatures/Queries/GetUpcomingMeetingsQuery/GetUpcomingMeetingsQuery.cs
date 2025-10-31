using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetUpcomingMeetingsQuery
{
    public class GetUpcomingMeetingsQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
