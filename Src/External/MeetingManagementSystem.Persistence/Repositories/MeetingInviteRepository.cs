using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class MeetingInviteRepository : GenericRepository<MeetingInvite, AppDbContext>, IMeetingInviteRepository
    {
        public MeetingInviteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
