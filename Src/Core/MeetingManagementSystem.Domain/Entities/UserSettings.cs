namespace MeetingManagementSystem.Domain.Entities
{
    public class UserSettings : BaseEntity
    {
        public bool ReceiveMeetingJoinNotifications { get; set; } = true;
        public bool ReceiveDecisionNotifications { get; set; } = true;
        
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
