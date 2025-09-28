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
        public CreateMeetingCommandHandler(IMeetingRepository meetingRepository, UserManager<AppUser> userManager)
        {
            _meetingRepository = meetingRepository;
            _userManager = userManager;
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
            var meeting = request.Adapt<Meeting>();
             _meetingRepository.Add(meeting);

            return new MessageResponse
            {
                Message = "Toplantı başarıyla oluşturuldu!",
                Success = true
            };
        }
    }
}
