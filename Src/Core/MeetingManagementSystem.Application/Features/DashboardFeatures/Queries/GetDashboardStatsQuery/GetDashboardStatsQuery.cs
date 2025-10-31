using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetDashboardStatsQuery
{
    public class GetDashboardStatsQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; } 
    }
}
