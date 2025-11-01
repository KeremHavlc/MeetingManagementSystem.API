using Mapster;
using MediatR;
using MeetingManagementSystem.Application.Abstractions;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand
{
    public class CreateDecisionAssignmentCommandHandler : IRequestHandler<CreateDecisionAssignmentCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDecisionRepository _decisionRepository;
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly IEmailSender _emailSender;

        public CreateDecisionAssignmentCommandHandler(
            IDecisionAssignmentRepository decisionAssignmentRepository,
            IDecisionRepository decisionRepository,
            UserManager<AppUser> userManager,
            IEmailSender emailSender,
            IUserSettingRepository userSettingRepository)
        {
            _decisionAssignmentRepository = decisionAssignmentRepository;
            _decisionRepository = decisionRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _userSettingRepository = userSettingRepository;
        }

        public async Task<MessageResponse> Handle(CreateDecisionAssignmentCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.DecisionId, out Guid decisionId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı decisionId formatı!",
                    Success = false
                };
            }

            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
                    Success = false
                };
            }
            //***//
            var existDecisionList = await _decisionRepository.GetWhereAsync(di => di.Id == decisionId);
            var decision = existDecisionList.FirstOrDefault();

            if (decision == null)
            {
                return new MessageResponse
                {
                    Message = "Karar bulunamadı!",
                    Success = false
                };
            }

            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if (existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    Success = false
                };
            }

            var existDecisionAssignment = await _decisionAssignmentRepository
                .GetWhereAsync(da => da.DecisionId == decisionId && da.UserId == userId);

            if (existDecisionAssignment.Any())
            {
                return new MessageResponse
                {
                    Message = "Bu user'a daha önceden decision atanmıştır!",
                    Success = false
                };
            }

            var decisionAssignment = request.Adapt<DecisionAssignment>();
            await _decisionAssignmentRepository.AddAsync(decisionAssignment);

            var userSettings = await _userSettingRepository.GetWhereAsync(us => us.AppUserId == userId);
            var setting = userSettings.FirstOrDefault();

            if (setting != null && setting.ReceiveDecisionNotifications)
            {
                string subject = "Yeni karar size atandı!";
                string body = $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Yeni Bir Karar Size Atandı 📌</title>
</head>
<body style='margin:0; padding:0; background-color:#f4f5f7; font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif;'>

  <center style='width:100%; background:#f4f5f7; padding:32px 16px;'>
    <table cellspacing='0' cellpadding='0' border='0' width='100%' style='max-width:640px; background-color:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);'>

      <!-- Üst renkli şerit -->
      <tr>
        <td style='background:#e63946; height:6px;'></td>
      </tr>

      <!-- Başlık -->
      <tr>
        <td style='padding:32px 24px 8px 24px; text-align:center;'>
          <h1 style='font-size:24px; margin:0; color:#111827;'>Yeni Bir Karar Size Atandı 📌</h1>
        </td>
      </tr>

      <!-- Kullanıcı -->
      <tr>
        <td style='padding:0 24px 8px 24px; text-align:center;'>
          <p style='margin:0; font-size:15px; color:#374151;'>
            Merhaba <b>{existUser.UserName}</b>,
          </p>
        </td>
      </tr>

      <!-- İçerik kutusu -->
      <tr>
        <td style='padding:24px;'>
          <table width='100%' cellpadding='0' cellspacing='0' border='0' style='background:#f9fafb; border:1px solid #e5e7eb; border-radius:10px;'>
            <tr>
              <td style='padding:20px;'>
                <p style='font-size:15px; color:#111827; margin:0 0 8px 0;'>
                  <b>{decision.Title}</b> başlıklı karar size atanmıştır.
                </p>
                <div style='border-left:4px solid #e63946; padding-left:10px; margin:8px 0;'>
                  <p style='margin:0; color:#4b5563; font-size:14px;'>
                    {decision.Description}
                  </p>
                </div>
                <p style='font-size:14px; color:#4b5563; margin:12px 0 0 0;'>
                  Karar detaylarını görmek için sisteme giriş yapabilirsiniz.
                </p>
              </td>
            </tr>
          </table>
        </td>
      </tr>

      <!-- Bildirim tercihi -->
      <tr>
        <td style='padding:12px 24px 0 24px; text-align:center;'>
          <p style='font-size:13px; color:#6b7280; margin:0;'>
            Bu tür e-postaları <b>Profil &gt; Bildirim Tercihleri</b> bölümünden yönetebilirsiniz.
          </p>
        </td>
      </tr>

      <!-- Alt bilgi -->
      <tr>
        <td style='padding:24px; text-align:center;'>
          <hr style='border:none; border-top:1px solid #e5e7eb; margin-bottom:12px;'>
          <p style='font-size:12px; color:#9ca3af; margin:0;'>
            Bu mail otomatik olarak gönderilmiştir. Lütfen yanıtlamayın.<br/>
            © {DateTime.Now:yyyy} Meeting Management System
          </p>
        </td>
      </tr>

    </table>
  </center>

</body>
</html>";


                await _emailSender.SendAsync(existUser.Email, subject, body);
            }

            return new MessageResponse
            {
                Message = "Karar ataması başarıyla tamamlandı!",
                Success = true 
            };
        }
    }
}
