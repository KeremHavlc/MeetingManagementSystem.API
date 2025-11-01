using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.CreateUserSettingsCommand
{
    public class CreateUserSettingsCommand : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
