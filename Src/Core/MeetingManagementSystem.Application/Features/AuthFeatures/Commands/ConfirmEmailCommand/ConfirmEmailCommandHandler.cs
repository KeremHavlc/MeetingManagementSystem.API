using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ConfirmEmailCommand
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmEmailCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new MessageResponse 
                { 
                    Success = false, 
                    Message = "Kullanıcı bulunamadı!" 
                };
            }
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Message = "E-posta doğrulama başarısız veya token geçersiz.",
                    Success = true
                };
            }
            return new MessageResponse
            {
                Message = "E-posta doğrulama başarılı!",
                Success = true
            };
        }
    }
}
