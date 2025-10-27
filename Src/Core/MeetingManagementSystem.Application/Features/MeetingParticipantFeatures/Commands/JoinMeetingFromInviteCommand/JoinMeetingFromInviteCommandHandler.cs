using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.JoinMeetingFromInviteCommand
{
    public class JoinMeetingFromInviteCommandHandler : IRequestHandler<JoinMeetingFromInviteCommand, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingRoleRepository _meetingRoleRepository;

        public JoinMeetingFromInviteCommandHandler(IMeetingRoleRepository meetingRoleRepository, UserManager<AppUser> userManager, IMeetingParticipantRepository meetingParticipantRepository, IMeetingRepository meetingRepository)
        {
            _meetingRoleRepository = meetingRoleRepository;
            _userManager = userManager;
            _meetingParticipantRepository = meetingParticipantRepository;
            _meetingRepository = meetingRepository;
        }

        public async Task<MessageResponse> Handle(JoinMeetingFromInviteCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.MeetingId, out Guid meetingId))
                return new MessageResponse { Message = "Geçersiz MeetingId!", Success = false };

            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
                return new MessageResponse { Message = "Toplantı bulunamadı!", Success = false };

            if (!Guid.TryParse(request.UserId, out Guid userId))
                return new MessageResponse { Message = "Geçersiz UserId!", Success = false };

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new MessageResponse { Message = "Kullanıcı bulunamadı!", Success = false };

            var existing = await _meetingParticipantRepository.GetSingleAsync(
                x => x.MeetingId == meetingId && x.UserId == userId);

            if (existing != null)
                return new MessageResponse { Message = "Zaten toplantı katılımcısısın.", Success = false };

            var participantRole = (await _meetingRoleRepository.GetWhereAsync(r => r.RoleName == "Participant"))
                .FirstOrDefault();

            if (participantRole == null)
                return new MessageResponse { Message = "Rol bulunamadı!", Success = false };

            var newParticipant = new MeetingParticipant
            {
                MeetingId = meetingId,
                UserId = userId,
                RoleId = participantRole.Id
            };

            await _meetingParticipantRepository.AddAsync(newParticipant);

            return new MessageResponse
            {
                Message = "Toplantıya başarıyla katıldınız!",
                Success = true
            };
        }
    }
}
