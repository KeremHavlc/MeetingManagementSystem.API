using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class DecisionRepository : GenericRepository<Decision, AppDbContext>, IDecisionRepository
    {
        public DecisionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
