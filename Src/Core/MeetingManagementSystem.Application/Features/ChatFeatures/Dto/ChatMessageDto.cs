namespace MeetingManagementSystem.Application.Features.ChatFeatures.Dto
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public Guid MeetingId { get; set; }
        public Guid SenderId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
