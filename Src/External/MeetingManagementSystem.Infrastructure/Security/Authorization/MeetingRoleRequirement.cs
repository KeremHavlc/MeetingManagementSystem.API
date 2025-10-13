using Microsoft.AspNetCore.Authorization;

namespace MeetingManagementSystem.Infrastructure.Security.Authorization
{
    public class MeetingRoleRequirement : IAuthorizationRequirement
    {
        public string RequiredRole { get; }

        public MeetingRoleRequirement(string requiredRole)
        {
            RequiredRole = requiredRole;
        }
    }
}
