using MeetingManagementSystem.Domain.Entities;

namespace MeetingManagementSystem.Domain.Repositories
{
    public interface IMeetingParticipantRepository : IGenericRepository<MeetingParticipant>
    {
        Task<List<MeetingParticipant>> GetMeetingParticipantsWithUsersAsync(Guid meetingId);
    }
}
