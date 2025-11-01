using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ConfirmEmailCommand
{
    public class ConfirmEmailCommand : IRequest<MessageResponse>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
