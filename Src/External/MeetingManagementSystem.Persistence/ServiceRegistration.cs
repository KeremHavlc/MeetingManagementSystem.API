using MeetingManagementSystem.Application.Services;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Persistence.Repositories;
using MeetingManagementSystem.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingManagementSystem.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection service)
        {            
            //Generic Repo Servisleri
            service.AddScoped<IMeetingRepository, MeetingRepository>();
            service.AddScoped<IMeetingParticipantRepository, MeetingParticipantRepository>();
            service.AddScoped<IDecisionRepository, DecisionRepository>();
            service.AddScoped<IDecisionAssignmentRepository, DecisionAssignmentRepository>();
            //AuthServis
            service.AddScoped<IAuthService, AuthService>();
        }
    }
}
