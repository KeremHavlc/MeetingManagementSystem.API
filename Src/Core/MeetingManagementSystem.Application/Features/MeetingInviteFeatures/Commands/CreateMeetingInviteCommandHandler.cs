using MediatR;
using MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Dto;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Commands
{
    public class CreateMeetingInviteCommandHandler : IRequestHandler<CreateMeetingInviteCommand, CreateMeetingInviteResponseDto>
    {
        private readonly IMeetingInviteRepository _meetingInviteRepository;

        public CreateMeetingInviteCommandHandler(IMeetingInviteRepository meetingInviteRepository)
        {
            _meetingInviteRepository = meetingInviteRepository;
        }

        public async Task<CreateMeetingInviteResponseDto> Handle(CreateMeetingInviteCommand request, CancellationToken cancellationToken)
        {
            var token = Guid.NewGuid().ToString("N");
            var invite = new MeetingInvite
            {
                MeetingId = request.MeetingId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddYears(1),
                IsUsed = false
            };
            await _meetingInviteRepository.AddAsync(invite);
            string link = $"https://meets.com.tr/invite/{token}";

            return new CreateMeetingInviteResponseDto
            {
                InviteLink = link,
                ExpiresAt = invite.ExpiresAt
            };
        }
    }
}
