using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserIdByUsernameOrEmailQuery
{
    public class GetUserIdByUsernameOrEmailQuery : IRequest<MessageResponse>
    {
        public string UsernameOrEmail { get; set; }
    }
}
