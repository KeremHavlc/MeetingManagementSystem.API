using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.CreateUserSettingsCommand
{
    public class CreateUserSettingsCommandHandler : IRequestHandler<CreateUserSettingsCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserSettingRepository _userSettingRepository;

        public CreateUserSettingsCommandHandler(UserManager<AppUser> userManager, IUserSettingRepository userSettingRepository)
        {
            _userManager = userManager;
            _userSettingRepository = userSettingRepository;
        }

        public async Task<MessageResponse> Handle(CreateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.UserId , out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId Formatı!",
                    Success = false
                };
            }
            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if(existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }
            var settings = new UserSettings
            {
                AppUserId = userId,
                ReceiveMeetingJoinNotifications = true,
                ReceiveDecisionNotifications = true
            };
            await _userSettingRepository.AddAsync(settings);
            return new MessageResponse
            {
                Message = "Kullanıcı ayarları başarıyla oluşturuldu!",
                Success = true,
            };
        }
    }
}
