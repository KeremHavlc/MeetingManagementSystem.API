namespace MeetingManagementSystem.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public Guid MeetingId { get; set; }
        public Guid SenderId { get; set; }
        public string Message { get; set; } = default!;
    }
}
