using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.UpdateUserSettingsCommand
{
    public class UpdateUserSettingsCommandHandler : IRequestHandler<UpdateUserSettingsCommand, MessageResponse>
    {
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserSettingsCommandHandler(IUserSettingRepository userSettingRepository, UserManager<AppUser> userManager)
        {
            _userSettingRepository = userSettingRepository;
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId Formatı!",
                    Success = false
                };
            }
            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if (existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }
            var settings = await _userSettingRepository.GetSingleAsync(ui => ui.AppUserId == userId);
            if (settings == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı ayarları bulunamadı!",
                    Success = false
                };
            }
            settings.ReceiveMeetingJoinNotifications = request.ReceiveMeetingJoinNotifications;
            settings.ReceiveDecisionNotifications = request.ReceiveDecisionNotifications;
            await _userSettingRepository.UpdateAsync(settings);

            return new MessageResponse
            {
                Message = "Ayarlar başarıyla güncellendi!",
                Success = true
            };
        }
    }
}
