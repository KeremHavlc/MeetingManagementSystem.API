using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ChangePasswordUsingTokenCommand
{
    public class ChangePasswordUsingTokenCommandHandler : IRequestHandler<ChangePasswordUsingTokenCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public ChangePasswordUsingTokenCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(ChangePasswordUsingTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }
           IdentityResult result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Message = string.Join(" ", result.Errors.Select(e => e.Description)),
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Şifre başarıyla değiştirildi!",
                Success = true
            };
        }
    }
}
