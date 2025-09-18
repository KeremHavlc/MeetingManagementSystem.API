using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class DecisionAssignmentRepository : GenericRepository<DecisionAssignment, AppDbContext>, IDecisionAssignmentRepository
    {
        public DecisionAssignmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
