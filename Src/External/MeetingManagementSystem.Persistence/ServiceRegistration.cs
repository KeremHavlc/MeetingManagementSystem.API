using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingManagementSystem.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection service)
        {            
            service.AddScoped<IMeetingRepository, MeetingRepository>();
            service.AddScoped<IMeetingParticipantRepository, MeetingParticipantRepository>();
            service.AddScoped<IDecisionRepository, DecisionRepository>();
            service.AddScoped<IDecisionAssignmentRepository, DecisionAssignmentRepository>();
            service.AddScoped<IMeetingRoleRepository, MeetingRoleRepository>();
            service.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            service.AddScoped<IMeetingInviteRepository, MeetingInviteRepository>();
        }
    }
}
