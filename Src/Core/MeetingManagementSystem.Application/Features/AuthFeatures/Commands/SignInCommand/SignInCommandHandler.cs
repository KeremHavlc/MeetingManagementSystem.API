using MediatR;
using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.SignInCommand
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;

        public SignInCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<MessageResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
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
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            if (result.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.Now;
                if(timeSpan is not null)
                {
                    return new MessageResponse
                    {
                        Message = $"Şifrenizi 3 kere yanlış girdiniz : {timeSpan.Value.TotalSeconds} Kilitlenmiştir.",
                        Success = false
                    };
                }
                else
                {
                    return new MessageResponse
                    {
                        Message = "Şifrenizi 3 kere yanlış girdiniz 30 saniye sonra tekrar deneyiniz.",
                        Success = false
                    };
                }
            }
            if (result.IsNotAllowed)
            {
                return new MessageResponse
                {
                    Message = "Mail adresiniz onaylı değildir!",
                    Success = false
                };
            }
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Message = "Şifreniz hatalıdır!",
                    Success = false
                };
            }
            var token = _jwtProvider.CreateToken(user);
            return new MessageResponse
            {
                Message = "Giriş Başarılı!",
                Success = true,
                Data = token
            };

        }
    }
}
