using MediatR;
using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<MessageResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == request.UserNameOrEmail || e.UserName == request.UserNameOrEmail);
            if(user == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }
            bool result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new MessageResponse
                {
                    Message = "Şifre Hatalıdır!",
                    Success = false
                };
            }
            var token = _jwtProvider.CreateToken(user);
            return new MessageResponse
            {
                Message = "Giriş başarılı!",
                Success = true,
                Data = token
            };
        }

    }
}
