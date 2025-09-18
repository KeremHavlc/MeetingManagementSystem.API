using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class MeetingParticipantRepository : GenericRepository<MeetingParticipant, AppDbContext>, IMeetingParticipantRepository
    {
        public MeetingParticipantRepository(AppDbContext context) : base(context)
        {
        }
    }
}
