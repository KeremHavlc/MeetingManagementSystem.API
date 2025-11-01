using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Queries.GetUserSettingsQuery
{
    public class GetUserSettingsQueryHandler : IRequestHandler<GetUserSettingsQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserSettingRepository _userSettingRepository;
        public GetUserSettingsQueryHandler(UserManager<AppUser> userManager, IUserSettingRepository userSettingRepository)
        {
            _userManager = userManager;
            _userSettingRepository = userSettingRepository;
        }

        public async Task<MessageResponse> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
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
            if(settings == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı ayarları bulunamadı!",
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Kullanıcı ayarları başarıyla bulundu!",
                Success = true,
                Data = new
                {
                    ReceiveMeetingJoinNotifications = settings.ReceiveMeetingJoinNotifications,
                    ReceiveDecisionNotifications = settings.ReceiveDecisionNotifications
                }
            };
        }
    }
}
