namespace MeetingManagementSystem.Domain.Entities
{
    public class MeetingInvite : BaseEntity
    {
        public Guid MeetingId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        public Meeting Meeting { get; set; } = null!;
    }
}
