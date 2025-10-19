namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Dtos
{
    public class DecisionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
