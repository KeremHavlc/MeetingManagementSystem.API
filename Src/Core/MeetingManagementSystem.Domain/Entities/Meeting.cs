namespace MeetingManagementSystem.Domain.Entities
{
    public class Meeting : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledAt { get; set; }
        public Guid CreatedByUserId { get; set; }

        public ICollection<Decision> Decisions { get; set; }
        public ICollection<MeetingParticipant>  MeetingParticipants{ get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<MeetingInvite> MeetingInvites { get; set; }
    }
}
