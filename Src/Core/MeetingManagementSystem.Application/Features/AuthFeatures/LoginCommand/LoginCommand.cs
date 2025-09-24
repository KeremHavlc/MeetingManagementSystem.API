using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.LoginCommand
{
    public class LoginCommand : IRequest<MessageResponse>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
