using Mapster;
using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.CreateMeetingCommand
{
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly IMeetingRoleRepository _meetingRoleRepository;
        public CreateMeetingCommandHandler(IMeetingRepository meetingRepository, UserManager<AppUser> userManager, IMeetingParticipantRepository meetingParticipantRepository, IMeetingRoleRepository meetingRoleRepository)
        {
            _meetingRepository = meetingRepository;
            _userManager = userManager;
            _meetingParticipantRepository = meetingParticipantRepository;
            _meetingRoleRepository = meetingRoleRepository;
        }

        public async Task<MessageResponse> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.CreatedByUserId.ToString());
            if(user == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }
            var userExistingMeetingTime = await _meetingRepository.GetWhereAsync(m => m.CreatedByUserId == request.CreatedByUserId);
            if (userExistingMeetingTime.Any(m => m.ScheduledAt == request.ScheduledAt))
            {
                return new MessageResponse
                {
                    Message = "Kullanıcının bu tarihte toplantısı mevcuttur!",
                    Success = false
                };
            }           
            //Toplantı oluşturma işlemi
            var meeting = request.Adapt<Meeting>();
            await _meetingRepository.AddAsync(meeting);
           
            //Toplantı oluşturulduğunda oluşturan kişinin otomatik olarak Admin Rolü ile Toplantı Katılımcılarına eklenmesi işlemleri
            var createdMeetingId = meeting.Id;
            var meetingRoles = await _meetingRoleRepository.GetAllAsync();
            var adminRole = meetingRoles.FirstOrDefault(rm => rm.RoleName == "Admin");

            if(adminRole == null)
            {
                return new MessageResponse
                {
                    Message = "Admin rolü bulunmamaktadır.",
                    Success = false
                };
            }

            var meetingParticipant = new MeetingParticipant
            {
                MeetingId = createdMeetingId,
                UserId = request.CreatedByUserId,
                RoleId = adminRole.Id
            };

            await _meetingParticipantRepository.AddAsync(meetingParticipant);

            return new MessageResponse
            {
                Message = "Toplantı başarıyla oluşturuldu!",
                Success = true
            };
        }
    }
}
