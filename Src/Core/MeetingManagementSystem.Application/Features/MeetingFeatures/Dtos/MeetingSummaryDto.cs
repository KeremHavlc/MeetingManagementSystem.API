namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Dtos
{
    public class MeetingSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledAt { get; set; }
        public Guid CreatedByUserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
