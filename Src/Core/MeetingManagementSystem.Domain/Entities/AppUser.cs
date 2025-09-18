using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => string.Join(" ", FirstName, LastName);

        public ICollection<Meeting> Meetings { get; set; }
        public ICollection<MeetingParticipant> MeetingParticipants { get; set; }
        public ICollection<DecisionAssignment> DecisionAssignments { get; set; }

    }
}
