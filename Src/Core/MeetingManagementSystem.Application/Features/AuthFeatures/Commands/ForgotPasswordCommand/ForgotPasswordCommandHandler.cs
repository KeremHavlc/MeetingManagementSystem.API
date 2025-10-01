using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public ForgotPasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.UserNameOrEmail || u.UserName == request.UserNameOrEmail);
            if(user == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return new MessageResponse
            {
                Message = "Token oluşturuldu",
                Success = true,
                Data = token
            };
        }
    }
}
