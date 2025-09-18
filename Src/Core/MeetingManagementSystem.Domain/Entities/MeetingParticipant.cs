namespace MeetingManagementSystem.Domain.Entities
{
    public class MeetingParticipant : BaseEntity
    {
        public Guid MeetingId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public Meeting Meeting { get; set; }
        public AppUser AppUser { get; set; }
        public AppRole AppRole { get; set; }
    }
}
