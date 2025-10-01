using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByNameRolesQuery
{
    public class GetByNameRolesQuery : IRequest<MessageResponse>
    {
        public string RoleName { get; set; }
    }
}
