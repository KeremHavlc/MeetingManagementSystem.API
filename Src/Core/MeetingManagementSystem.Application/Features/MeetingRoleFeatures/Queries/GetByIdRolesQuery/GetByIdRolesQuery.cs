using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByIdRolesQuery
{
    public class GetByIdRolesQuery : IRequest<MessageResponse>
    {
        public string RoleId { get; set; }
    }
}
