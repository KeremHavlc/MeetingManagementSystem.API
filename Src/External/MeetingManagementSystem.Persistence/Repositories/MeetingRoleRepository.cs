using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class MeetingRoleRepository : GenericRepository<MeetingRole, AppDbContext>, IMeetingRoleRepository
    {
        public MeetingRoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
