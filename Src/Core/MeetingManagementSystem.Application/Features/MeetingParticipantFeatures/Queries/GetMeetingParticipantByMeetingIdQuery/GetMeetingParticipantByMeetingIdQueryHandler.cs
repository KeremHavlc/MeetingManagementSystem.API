using MediatR;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByMeetingIdQuery
{
    public class GetMeetingParticipantByMeetingIdQueryHandler : IRequestHandler<GetMeetingParticipantByMeetingIdQuery, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingRoleRepository _meetingRoleRepository;

        public GetMeetingParticipantByMeetingIdQueryHandler(IMeetingParticipantRepository meetingParticipantRepository, IMeetingRepository meetingRepository, IMeetingRoleRepository meetingRoleRepository, UserManager<AppUser> userManager)
        {
            _meetingParticipantRepository = meetingParticipantRepository;
            _meetingRepository = meetingRepository;
            _meetingRoleRepository = meetingRoleRepository;
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(GetMeetingParticipantByMeetingIdQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.MeetingId , out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }
            var existMeeting = await _meetingRepository.GetSingleAsync(me => me.Id == meetingId);
            if(existMeeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı Bulunamadı!",
                    Success = false
                };
            }
            var meetingParticipant = await _meetingParticipantRepository.GetWhereAsync(mp => mp.MeetingId == meetingId);
            if (meetingParticipant == null || !meetingParticipant.Any())
            {
                return new MessageResponse
                {
                    Message = "Toplantı Katılımcısı bulunmamaktadır",
                    Success = false
                };
            }
            var participantDtos = new List<MeetingParticipantDto>();
            foreach (var mp in meetingParticipant)
            {
                var user = await _userManager.FindByIdAsync(mp.UserId.ToString());
                var role = await _meetingRoleRepository.GetSingleAsync(r => r.Id == mp.RoleId);

                participantDtos.Add(new MeetingParticipantDto
                {
                    UserId = mp.UserId,
                    Username = user?.UserName ?? "Bilinmiyor",
                    RoleId = mp.RoleId,
                    RoleName = role?.RoleName ?? "Bilinmiyor"

                });
            }
            return new MessageResponse
            {
                Message = "Toplantı katılımcıları başarıyla bulunmuştur!",
                Success = true,
                Data = participantDtos
            };
        }
    }
}
