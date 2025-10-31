namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Dtos
{
    public class DashboardSummaryDto
    {        
            public int TotalMeetings { get; set; }
            public int TotalDecisions { get; set; }
            public int ActiveAssignments { get; set; }
            public double CompletionRate { get; set; }

            public List<MeetingStatDto> MonthlyMeetingStats { get; set; } = new();
            public List<UpcomingMeetingDto> UpcomingMeetings { get; set; } = new();              
    }
}
