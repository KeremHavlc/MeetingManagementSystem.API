using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserIdByUsernameOrEmailQuery
{
    public class GetUserIdByUsernameOrEmailQueryHandler : IRequestHandler<GetUserIdByUsernameOrEmailQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserIdByUsernameOrEmailQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(GetUserIdByUsernameOrEmailQuery request, CancellationToken cancellationToken)
        {
            AppUser user;

            if (request.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            }

            if (user == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }

            return new MessageResponse
            {
                Message = "Kullanıcı Başarıyla Bulundu!",
                Success = true,
                Data = new
                {
                    userId = user.Id,
                    userName = user.UserName,
                    email = user.Email
                }
            };
        }
    }
}
