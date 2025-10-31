using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserInfoQuery
{
    public class GetUserInfoQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
