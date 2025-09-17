namespace MeetingManagementSystem.Domain.Entities
{
    public class MeetingParticipant : BaseEntity
    {
        public Guid MeetingId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }

        public Meeting Meeting { get; set; }
    }
}
