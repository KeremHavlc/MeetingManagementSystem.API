namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.Dtos
{
    public class MeetingParticipantDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }    
}
