using MeetingManagementSystem.Domain.Enums;

namespace MeetingManagementSystem.Domain.Entities
{
    public class DecisionAssignment : BaseEntity
    {
        public Guid DecisionId { get; set; }
        public Guid UserId { get; set; }
        public DecisionStatusEnum DecisionStatus { get; set; }
        public Decision Decision { get; set; }
        public AppUser AppUser { get; set; }
    }
 
}
