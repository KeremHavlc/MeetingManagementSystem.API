using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.UpdateUserSettingsCommand
{
    public class UpdateUserSettingsCommand : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
        public bool ReceiveMeetingJoinNotifications { get; set; }
        public bool ReceiveDecisionNotifications { get; set; }
    }
}
