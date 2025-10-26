using MediatR;

namespace MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Queries
{
    public class ValidateInviteTokenQuery : IRequest<ValidateInviteTokenResponseDto>
    {
        public string Token { get; set; }
    }
}
