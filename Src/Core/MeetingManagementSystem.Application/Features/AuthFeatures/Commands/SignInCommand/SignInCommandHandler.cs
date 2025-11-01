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
        private readonly IEmailSender _emailSender;

        public SignInCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtProvider jwtProvider,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _emailSender = emailSender;
        }

        public async Task<MessageResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == request.UserNameOrEmail || u.UserName == request.UserNameOrEmail);

            if (user == null)
                return new MessageResponse { Success = false, Message = "Kullanıcı bulunamadı!" };

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (result.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.Now;
                return new MessageResponse
                {
                    Success = false,
                    Message = $"Şifrenizi 3 kere yanlış girdiniz. {(timeSpan is not null ? $"{Math.Round(timeSpan.Value.TotalSeconds)} saniye" : "Kısa bir süre")} sonra tekrar deneyin."
                };
            }

            if (!user.EmailConfirmed)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = $"http://localhost:5173/verify-email?email={user.Email}&token={Uri.EscapeDataString(token)}";

                var htmlBody = $@"
                <h2>Hesabını Doğrula</h2>
                <p>Merhaba {user.UserName}, hesabını aktif hale getirmek için aşağıdaki bağlantıya tıklayabilirsin.</p>
                <a href='{link}' style='background:#e63946;color:#fff;padding:10px 18px;border-radius:6px;text-decoration:none;'>E-postamı Doğrula</a>
                ";

                await _emailSender.SendAsync(user.Email, "E-posta Doğrulama", htmlBody);

                return new MessageResponse
                {
                    Success = false,
                    Message = "E-posta adresin doğrulanmamış. Doğrulama linki yeniden mail adresine gönderildi."
                };
            }

            if (!result.Succeeded)
                return new MessageResponse { Success = false, Message = "Şifreniz hatalıdır!" };
            
            var tokenJwt = _jwtProvider.CreateToken(user);

            return new MessageResponse
            {
                Success = true,
                Message = "Giriş başarılı!",
                Data = tokenJwt
            };
        }
    }
}
