namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Dtos
{
    public class UpcomingMeetingDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledAt { get; set; }
    }
}
