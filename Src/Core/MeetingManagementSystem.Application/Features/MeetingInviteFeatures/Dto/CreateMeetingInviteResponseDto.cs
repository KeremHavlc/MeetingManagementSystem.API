namespace MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Dto
{
    public class CreateMeetingInviteResponseDto
    {
        public string InviteLink { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
