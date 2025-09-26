namespace MeetingManagementSystem.Domain.Entities
{
    public class MeetingRole : BaseEntity
    {
        public string RoleName { get; set; }
        public ICollection<MeetingParticipant> MeetingParticipants { get; set; }
    }
}
