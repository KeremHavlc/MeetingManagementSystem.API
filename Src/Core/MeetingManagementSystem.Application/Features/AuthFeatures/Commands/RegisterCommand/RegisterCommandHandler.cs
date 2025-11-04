using Mapster;
using MediatR;
using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public RegisterCommandHandler(UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<AppUser>();

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = string.Join(" ", result.Errors.Select(e => e.Description))
                };
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = $"https://meets.com.tr/verify-email?email={user.Email}&token={Uri.EscapeDataString(token)}";



            var htmlBody = $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
<meta charset='UTF-8'>
<title>E-posta Doğrulama</title>
<style>
  body {{
    background-color: #f6f8fb;
    font-family: 'Segoe UI', sans-serif;
    color: #333;
    margin: 0;
    padding: 0;
  }}
  .container {{
    max-width: 600px;
    margin: 40px auto;
    background: #ffffff;
    border-radius: 12px;
    box-shadow: 0 6px 20px rgba(0,0,0,0.1);
    padding: 40px;
  }}
  .logo {{
    text-align: center;
    font-size: 26px;
    font-weight: bold;
    color: #e63946;
    margin-bottom: 20px;
  }}
  h2 {{
    text-align: center;
    color: #222;
  }}
  p {{
    line-height: 1.6;
    font-size: 15px;
    color: #444;
  }}
  .btn {{
    display: block;
    width: fit-content;
    margin: 25px auto;
    background-color: #e63946;
    color: white !important;
    text-decoration: none;
    padding: 12px 28px;
    border-radius: 6px;
    font-weight: bold;
    letter-spacing: 0.4px;
    transition: background 0.3s ease;
  }}
  .btn:hover {{
    background-color: #d62839;
  }}
  .footer {{
    margin-top: 40px;
    font-size: 13px;
    color: #777;
    text-align: center;
    border-top: 1px solid #eee;
    padding-top: 15px;
  }}
</style>
</head>
<body>
  <div class='container'>
    <div class='logo'>Meeting Management System</div>
    <h2>Hesabını Aktif Et</h2>
    <p>Merhaba <strong>{user.UserName}</strong>,</p>
    <p>
      Hesabını aktif hale getirmek için aşağıdaki butona tıklayarak e-posta adresini doğrula.
    </p>

    <a href='{link}' class='btn'>E-postamı Doğrula</a>

    <p>Eğer bu işlemi sen başlatmadıysan, bu e-postayı görmezden gelebilirsin.</p>
    <div class='footer'>
      © {DateTime.Now.Year} Meeting Management System — Tüm hakları saklıdır.
    </div>
  </div>
</body>
</html>
";

            await _emailSender.SendAsync(user.Email, "E-posta Doğrulama", htmlBody);

            return new MessageResponse
            {
                Success = true,
                Message = "Kullanıcı başarıyla kayıt edildi! E-posta doğrulama linki gönderildi."
            };
        }
    }
}
