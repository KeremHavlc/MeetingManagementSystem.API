using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Commands.UpdateUserCommand
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
            existUser.FirstName = request.FirstName ?? existUser.FirstName;            
            existUser.LastName = request.LastName ?? existUser.LastName;            
            existUser.UserName = request.UserName ?? existUser.UserName;

            var result = await _userManager.UpdateAsync(existUser);
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)),
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Kullanıcı bilgileri başarıyla güncellendi!",
                Success = true,
                Data = new
                {
                    existUser.Id,
                    existUser.FirstName,
                    existUser.LastName,
                    existUser.UserName,
                    existUser.Email
                }
            };
        }
    }
}
