using MeetingManagementSystem.Domain.Entities;

namespace MeetingManagementSystem.Domain.Repositories
{
    public interface IChatMessageRepository : IGenericRepository<ChatMessage>
    {
        Task<ChatMessage> AddAsync(ChatMessage entity, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ChatMessage>> GetMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default);
    }
}
