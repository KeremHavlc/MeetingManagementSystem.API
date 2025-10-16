using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class MeetingParticipantRepository : GenericRepository<MeetingParticipant, AppDbContext>, IMeetingParticipantRepository
    {
        private readonly AppDbContext _context;
        public MeetingParticipantRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MeetingParticipant>> GetMeetingParticipantsWithUsersAsync(Guid meetingId)
        {
            return await _context.MeetingParticipants
                .Include(mp => mp.AppUser)
                .Where(mp => mp.MeetingId == meetingId)
                .ToListAsync();
        }
    }
}
