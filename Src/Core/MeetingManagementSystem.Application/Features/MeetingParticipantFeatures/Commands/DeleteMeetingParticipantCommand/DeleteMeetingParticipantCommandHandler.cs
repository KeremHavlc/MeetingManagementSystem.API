using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.DeleteMeetingParticipantCommand
{
    public class DeleteMeetingParticipantCommandHandler : IRequestHandler<DeleteMeetingParticipantCommand, MessageResponse>
    {

        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingRoleRepository _meetingRoleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;



        public DeleteMeetingParticipantCommandHandler(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IMeetingRoleRepository meetingRoleRepository, IMeetingRepository meetingRepository, IMeetingParticipantRepository meetingParticipantRepository)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _meetingRoleRepository = meetingRoleRepository;
            _meetingRepository = meetingRepository;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        public async Task<MessageResponse> Handle(DeleteMeetingParticipantCommand request, CancellationToken cancellationToken)
        {            
            if (!Guid.TryParse(request.DeleteUserId, out Guid deleteUserId))
            {
                return new MessageResponse { Message = "Geçersiz userId formatı", Success = false };
            }

            if (!Guid.TryParse(request.MeetingId, out Guid meetingId))
            {
                return new MessageResponse { Message = "Geçersiz meetingId formatı", Success = false };
            }

            var currentUserIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(currentUserIdClaim) || !Guid.TryParse(currentUserIdClaim, out Guid currentUserId))
            {
                return new MessageResponse { Message = "Yetkisiz kullanıcı!", Success = false };
            }

            var existDeleteUser = await _userManager.FindByIdAsync(request.DeleteUserId);
            if (existDeleteUser == null)
            {
                return new MessageResponse { Message = "Silinmek istenen kullanıcı bulunamadı!", Success = false };
            }

            var existMeeting = await _meetingRepository.GetSingleAsync(m => m.Id == meetingId);
            if (existMeeting == null)
            {
                return new MessageResponse { Message = "Toplantı bulunamadı!", Success = false };
            }

            var currentParticipant = (await _meetingParticipantRepository.GetWhereAsync(mp => mp.UserId == currentUserId && mp.MeetingId == meetingId)).FirstOrDefault();
            if(currentParticipant == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı toplantıda bulunamadı!",
                    Success = false
                };
            }

            var currentRole = await _meetingRoleRepository.GetByIdAsync(currentParticipant.RoleId);
            if(currentRole == null)
            {
                return new MessageResponse
                {
                    Message = "Rol bilgisi alınamadı!",
                    Success = false
                };
            }

            var deleteParticipant = (await _meetingParticipantRepository.GetWhereAsync(mp => mp.UserId == deleteUserId && mp.MeetingId == meetingId)).FirstOrDefault();
            if(deleteParticipant == null)
            {
                return new MessageResponse
                {
                    Message = "Silinmek istenen kullanıcı toplantıda bulunamadı!",
                    Success = false
                };
            }

            var deleteRole = await _meetingRoleRepository.GetByIdAsync(deleteParticipant.RoleId);
            if(deleteRole == null)
            {
                return new MessageResponse
                {
                    Message = "Rol Bilgisi alınamadı!",
                    Success = false
                };
            }

            if(currentRole.RoleName == "Admin")
            {
                await _meetingParticipantRepository.RemoveAsync(deleteParticipant.Id);
            }
            else if(currentRole.RoleName == "Moderator")
            {
                if(deleteRole.RoleName == "Admin")
                {
                    return new MessageResponse
                    {
                        Message = "Admin kullanıcısını silmeye yetkiniz yoktur!",
                        Success = false
                    };
                }
                await _meetingParticipantRepository.RemoveAsync(deleteParticipant.Id);

            }
            else
            {
                return new MessageResponse
                {
                    Message = "Bu işlemi yapmaya yetkiniz yoktur!",
                    Success = false
                };
            }
            if (deleteUserId == currentUserId)
            {
                return new MessageResponse
                {
                    Message = "Kendi kendinizi silemezsiniz!",
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Katılımcı başarıyla silindi.",
                Success = true
            };
        }
    }
}
