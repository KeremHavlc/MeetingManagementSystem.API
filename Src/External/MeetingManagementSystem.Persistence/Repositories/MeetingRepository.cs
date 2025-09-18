using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, AppDbContext>, IMeetingRepository
    {
        public MeetingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
