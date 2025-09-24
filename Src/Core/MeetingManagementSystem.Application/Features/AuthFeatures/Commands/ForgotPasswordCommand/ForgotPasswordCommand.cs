using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommand : IRequest<MessageResponse>
    {
        public string UserNameOrEmail { get; set; }
    }
}
