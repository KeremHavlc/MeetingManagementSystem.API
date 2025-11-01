using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Context;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class UserSettingsRepository : GenericRepository<UserSettings, AppDbContext>, IUserSettingRepository
    {
        public UserSettingsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
