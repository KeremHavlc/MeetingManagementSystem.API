using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.CreateMeetingRoleCommand
{
    public class CreateMeetingRoleCommand : IRequest<MessageResponse>
    {
        public string RoleName { get; set; }
    }
}
