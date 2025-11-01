using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Application.Common;
using MeetingManagementSystem.Infrastructure.EmailService;
using MeetingManagementSystem.Infrastructure.RealTime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace MeetingManagementSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void  AddInfrastructureService(this IServiceCollection service , IConfiguration configuration)
        {
            service.AddScoped<IChatNotifier, ChatNotifier>();
            service.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            service.AddScoped<IEmailSender, SmtpEmailSender>();
        }
    }
}
