using Mapster;
using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.AddMeetingParticipantCommand
{
    public class AddMeetingParticipantCommandHandler : IRequestHandler<AddMeetingParticipantCommand, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingRoleRepository _meetingRoleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        public AddMeetingParticipantCommandHandler(
            IMeetingRepository meetingRepository,
            IMeetingParticipantRepository meetingParticipantRepository,
            UserManager<AppUser> userManager,
            IMeetingRoleRepository meetingRoleRepository,
            IHttpContextAccessor httpContextAccessor) 
        {
            _meetingRepository = meetingRepository;
            _meetingParticipantRepository = meetingParticipantRepository;
            _userManager = userManager;
            _meetingRoleRepository = meetingRoleRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<MessageResponse> Handle(AddMeetingParticipantCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.MeetingId, out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }

            var existMeeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (existMeeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı bulunamadı!",
                    Success = false
                };
            }

            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
                    Success = false
                };
            }

            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if (existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    Success = false
                };
            }

            var existMeetingParticipant = await _meetingParticipantRepository.GetSingleAsync(uid => uid.MeetingId == meetingId && uid.UserId == userId);

            if (existMeetingParticipant != null)
            {
                return new MessageResponse
                {
                    Message = "Bu kullanıcı zaten toplantıda katılımcıdır.",
                    Success = false
                };
            }
            // JWT'den userId al
            var currentUserIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(currentUserIdClaim))
                return new MessageResponse { Message = "Yetkisiz kullanıcı!", Success = false };

            if (!Guid.TryParse(currentUserIdClaim, out Guid currentUserId))
                return new MessageResponse { Message = "Geçersiz kullanıcı Id!", Success = false };

            // MeetingParticipant var mı kontrol
            var currentParticipant = (await _meetingParticipantRepository.GetWhereAsync(
                mp => mp.MeetingId == meetingId && mp.UserId == currentUserId)).FirstOrDefault();

            if (currentParticipant == null)
                return new MessageResponse { Message = "Toplantıya katılımcı değilsin!", Success = false };

            // Rolü ayrı çek
            var meetingRole = await _meetingRoleRepository.GetByIdAsync(currentParticipant.RoleId);
            if (meetingRole == null)
                return new MessageResponse { Message = "Rol bilgisi bulunamadı!", Success = false };

            // Yetki kontrolü
            if (meetingRole.RoleName != "Admin" && meetingRole.RoleName != "Moderator")
                return new MessageResponse { Message = "Bu işlemi yapmaya yetkin yok!", Success = false };


            var roleEntity = (await _meetingRoleRepository.GetWhereAsync(r => r.RoleName == "Participant"))
                .FirstOrDefault();

            if (roleEntity == null)
                return new MessageResponse { Message = "Participant rolü bulunamadı!", Success = false };

            var meetingParticipant = new MeetingParticipant
            {
                MeetingId = meetingId,
                UserId = userId,
                RoleId = roleEntity.Id
            };
            await _meetingParticipantRepository.AddAsync(meetingParticipant);
            return new MessageResponse
            {
                Message = "Toplantı katılımcısı başarıyla eklendi!",
                Success = true
            };
        }
    }
}
