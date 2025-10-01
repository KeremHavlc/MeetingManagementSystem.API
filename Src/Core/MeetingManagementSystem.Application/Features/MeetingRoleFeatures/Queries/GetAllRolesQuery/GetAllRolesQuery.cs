using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetAllRolesQuery
{
    public class GetAllRolesQuery : IRequest<MessageResponse>
    {
    }
}
