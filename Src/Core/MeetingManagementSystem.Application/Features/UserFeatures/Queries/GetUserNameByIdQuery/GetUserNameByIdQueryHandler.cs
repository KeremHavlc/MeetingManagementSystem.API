using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserNameByIdQuery
{
    public class GetUserNameByIdQueryHandler : IRequestHandler<GetUserNameByIdQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserNameByIdQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(GetUserNameByIdQuery request, CancellationToken cancellationToken)
        {
            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if (existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı",
                    Success = false,
                    Data = null
                };
            }

            return new MessageResponse
            {
                Message = "Kullanıcı başarıyla bulundu",
                Success = true,
                Data = new
                {
                    UserId = existUser.Id,
                    UserName = existUser.UserName,
                    FullName = $"{existUser.FirstName} {existUser.LastName}",
                    Email = existUser.Email
                }
            };
        }
    }
}
