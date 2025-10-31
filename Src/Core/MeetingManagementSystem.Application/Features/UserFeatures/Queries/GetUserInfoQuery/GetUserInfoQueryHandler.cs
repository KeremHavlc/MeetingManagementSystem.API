using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserInfoQuery
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserInfoQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.UserId , out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
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
            return new MessageResponse
            {
                Message = "Kullanıcı bilgileri başarıyla bulundu!",
                Success = true,
                Data = new
                {
                    FirstName = existUser.FirstName,
                    LastName = existUser.LastName,
                    UserName = existUser.UserName,
                    Email = existUser.Email
                }
            };
        }
    }
}
