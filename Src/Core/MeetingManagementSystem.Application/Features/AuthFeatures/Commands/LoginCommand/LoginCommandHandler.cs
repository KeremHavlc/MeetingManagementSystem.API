using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MessageResponseDto>
    {
        private readonly UserManager<AppUser> _userManager;

        public LoginCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(un => un.UserName == request.UserNameOrEmail || un.Email == request.UserNameOrEmail);
            if(user == null)
            {
                return new MessageResponseDto
                {
                    Success = false,
                    Message = "Kullanıcı Bulunamadı!"
                };
            }
            bool result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new MessageResponseDto
                {
                    Success = false,
                    Message = "Şifreniz Hatalıdır!"
                };
            }
            return new MessageResponseDto
            {
                Success = true,
                Message = "Giriş Başarılıdır!"
            };
        }
    }
}
