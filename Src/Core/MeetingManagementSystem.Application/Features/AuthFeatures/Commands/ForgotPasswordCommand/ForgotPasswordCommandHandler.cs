using MediatR;
using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordCommandHandler(UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<MessageResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == request.UserNameOrEmail || u.UserName == request.UserNameOrEmail, cancellationToken);

            if (user == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    Success = false
                };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = $"https://meeting-management-system-client.vercel.app/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";


            var htmlBody = $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
  <meta charset='UTF-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1.0' />
  <title>Şifre Sıfırlama</title>
  <style>
    body {{
      background-color: #f6f8fb;
      font-family: 'Segoe UI', Arial, sans-serif;
      color: #333;
      margin: 0;
      padding: 0;
    }}
    .container {{
      max-width: 600px;
      margin: 40px auto;
      background-color: #ffffff;
      border-radius: 12px;
      box-shadow: 0 6px 20px rgba(0,0,0,0.1);
      padding: 40px;
    }}
    .logo {{
      text-align: center;
      font-size: 24px;
      font-weight: bold;
      color: #e63946;
      margin-bottom: 25px;
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
    <h2>Şifre Sıfırlama Talebi</h2>
    <p>Merhaba <strong>{user.UserName}</strong>,</p>
    <p>
      Hesabın için bir şifre sıfırlama talebi alındı.
      Eğer bu işlemi sen başlattıysan, aşağıdaki butona tıklayarak yeni bir şifre belirleyebilirsin.
    </p>

    <a href='{link}' class='btn'>Şifremi Sıfırla</a>

    <p>Eğer bu talebi sen oluşturmadıysan, bu e-postayı görmezden gelebilirsin.</p>
    <div class='footer'>
      © {DateTime.Now.Year} Meeting Management System — Tüm hakları saklıdır.
    </div>
  </div>
</body>
</html>
";

            await _emailSender.SendAsync(user.Email, "Şifre Sıfırlama", htmlBody);

            return new MessageResponse
            {
                Message = "Şifre sıfırlama maili gönderildi.",
                Success = true,
                Data = token
            };
        }
    }
}
