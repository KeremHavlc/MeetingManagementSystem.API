using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Domain.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<MeetingParticipant> MeetingParticipants { get; set; }
    }
}
