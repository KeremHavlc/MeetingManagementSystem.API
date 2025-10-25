using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class ChatMessageRepository : GenericRepository<ChatMessage, AppDbContext>, IChatMessageRepository
    {
        private readonly AppDbContext _context;
        public ChatMessageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ChatMessage> AddAsync(ChatMessage entity, CancellationToken cancellationToken = default)
        {
            await _context.ChatMessage.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IReadOnlyList<ChatMessage>> GetMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default)
        {
            return await _context.ChatMessage
                .Where(x => x.MeetingId == meetingId && x.IsActive)
                .OrderBy(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
