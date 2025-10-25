using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserNameByIdQuery
{
    public class GetUserNameByIdQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
