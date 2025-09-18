namespace MeetingManagementSystem.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } 
    }
}
