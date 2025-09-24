using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ChangePasswordUsingTokenCommand
{
    public class ChangePasswordUsingTokenCommand : IRequest<MessageResponse>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
