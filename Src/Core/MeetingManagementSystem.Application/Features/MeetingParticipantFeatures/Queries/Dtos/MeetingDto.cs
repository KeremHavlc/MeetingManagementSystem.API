namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.Dtos
{
    public class MeetingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledAt { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
