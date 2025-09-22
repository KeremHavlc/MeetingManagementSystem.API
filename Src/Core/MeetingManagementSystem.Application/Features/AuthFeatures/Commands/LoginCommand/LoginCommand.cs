using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand
{
    public class LoginCommand : IRequest<MessageResponseDto>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
