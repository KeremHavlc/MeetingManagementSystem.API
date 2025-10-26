using MediatR;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Queries
{
    public class ValidateInviteTokenQueryHandler : IRequestHandler<ValidateInviteTokenQuery, ValidateInviteTokenResponseDto>
    {
        private readonly IMeetingInviteRepository _meetingInviteRepository;

        public ValidateInviteTokenQueryHandler(IMeetingInviteRepository meetingInviteRepository)
        {
            _meetingInviteRepository = meetingInviteRepository;
        }

        public async Task<ValidateInviteTokenResponseDto> Handle(ValidateInviteTokenQuery request, CancellationToken cancellationToken)
        {
            var invite = await _meetingInviteRepository.GetSingleAsync(x => x.Token == request.Token);
            if (invite == null || invite.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Geçersiz veya süresi dolmuş davet linki!");

            return new ValidateInviteTokenResponseDto
            {
                MeetingId = invite.MeetingId
            };
        }
    }
}
