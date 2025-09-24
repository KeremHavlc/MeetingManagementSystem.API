using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.SignInCommand
{
    public class SignInCommand : IRequest<MessageResponse>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
