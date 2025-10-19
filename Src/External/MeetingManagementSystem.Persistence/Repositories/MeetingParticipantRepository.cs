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

        public async Task<List<Object>> GetMeetingParticipantsWithUsersAsync(Guid meetingId)
        {
            return await _context.MeetingParticipants
                .Where(mp => mp.MeetingId == meetingId)
                .Select(mp => new
                {
                    mp.Id,
                    mp.RoleId,
                    mp.MeetingId,
                    AppUser = new
                    {
                        mp.AppUser.Id,
                        mp.AppUser.UserName,
                        mp.AppUser.Email
                    }
        })
        .ToListAsync<object>();
        }
    }
}
