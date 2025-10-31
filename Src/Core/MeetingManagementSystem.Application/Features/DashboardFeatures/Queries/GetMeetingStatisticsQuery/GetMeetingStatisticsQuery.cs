using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetMeetingStatisticsQuery
{
    public class GetMeetingStatisticsQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
        public string Period { get; set; } = "month"; 
    }
}
