using MeetingManagementSystem.Application.Common;
using MeetingManagementSystem.Infrastructure.RealTime;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingManagementSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void  AddInfrastructureService(this IServiceCollection service)
        {
            service.AddScoped<IChatNotifier, ChatNotifier>();
        }
    }
}
