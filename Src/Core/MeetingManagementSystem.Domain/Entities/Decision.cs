namespace MeetingManagementSystem.Domain.Entities
{
    public class Decision : BaseEntity
    {
        public Guid MeetingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Meeting Meeting { get; set; }
        public ICollection<DecisionAssignment> DecisionAssignments { get; set; }
    }
}
