using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Queries.GetUserSettingsQuery
{
    public class GetUserSettingsQuery : IRequest<MessageResponse>
    {
        public string UserId { get; set; }
    }
}
