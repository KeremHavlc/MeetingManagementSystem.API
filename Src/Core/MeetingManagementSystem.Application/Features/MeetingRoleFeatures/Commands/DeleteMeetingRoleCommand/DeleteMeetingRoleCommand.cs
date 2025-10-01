using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.DeleteMeetingRoleCommand
{
    public class DeleteMeetingRoleCommand : IRequest<MessageResponse>
    {
        public string RoleId { get; set; }
    }
}
