using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
// 🔹 EKLENDİ
using MeetingManagementSystem.Application.Abstractions;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.JoinMeetingFromInviteCommand
{
    public class JoinMeetingFromInviteCommandHandler : IRequestHandler<JoinMeetingFromInviteCommand, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingRoleRepository _meetingRoleRepository;
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly IEmailSender _emailSender;

        public JoinMeetingFromInviteCommandHandler(
            IMeetingRoleRepository meetingRoleRepository,
            UserManager<AppUser> userManager,
            IMeetingParticipantRepository meetingParticipantRepository,
            IMeetingRepository meetingRepository,
            IUserSettingRepository userSettingRepository,   
            IEmailSender emailSender                        
        )
        {
            _meetingRoleRepository = meetingRoleRepository;
            _userManager = userManager;
            _meetingParticipantRepository = meetingParticipantRepository;
            _meetingRepository = meetingRepository;
            _userSettingRepository = userSettingRepository; 
            _emailSender = emailSender;                     
        }

        public async Task<MessageResponse> Handle(JoinMeetingFromInviteCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.MeetingId, out Guid meetingId))
                return new MessageResponse { Message = "Geçersiz MeetingId!", Success = false };

            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
                return new MessageResponse { Message = "Toplantı bulunamadı!", Success = false };

            if (!Guid.TryParse(request.UserId, out Guid userId))
                return new MessageResponse { Message = "Geçersiz UserId!", Success = false };

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new MessageResponse { Message = "Kullanıcı bulunamadı!", Success = false };

            var existing = await _meetingParticipantRepository.GetSingleAsync(
                x => x.MeetingId == meetingId && x.UserId == userId);

            if (existing != null)
                return new MessageResponse { Message = "Zaten toplantı katılımcısısın.", Success = false };

            var participantRole = (await _meetingRoleRepository.GetWhereAsync(r => r.RoleName == "Participant"))
                .FirstOrDefault();

            if (participantRole == null)
                return new MessageResponse { Message = "Rol bulunamadı!", Success = false };

            var newParticipant = new MeetingParticipant
            {
                MeetingId = meetingId,
                UserId = userId,
                RoleId = participantRole.Id
            };

            await _meetingParticipantRepository.AddAsync(newParticipant);

            var userSetting = await _userSettingRepository.GetSingleAsync(s => s.AppUserId == userId);
            if (userSetting != null && userSetting.ReceiveMeetingJoinNotifications)
            {
                string subject = $"'{meeting.Title}' toplantısına eklendiniz!";

                string body = $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Toplantıya Katıldınız 🎉</title>
</head>
<body style='margin:0; padding:0; background-color:#f2f2f2;'>
  <center style='width:100%; background:#f2f2f2; padding:20px 0;'>
    <table cellpadding='0' cellspacing='0' border='0' width='600' style='background:#ffffff; border-collapse:collapse; border-radius:10px; overflow:hidden;'>
      <tr>
        <td bgcolor='#e63946' height='6' style='line-height:6px; font-size:0;'>&nbsp;</td>
      </tr>
      <tr>
        <td align='center' style='padding:25px 15px 10px 15px;'>
          <strong style='font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif; font-size:22px; color:#111111;'>
            Toplantıya Katıldınız 🎉
          </strong>
        </td>
      </tr>
      <tr>
        <td align='center' style='padding:5px 15px 20px 15px;'>
          <span style='font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif; font-size:15px; color:#333333;'>
            Merhaba <b>{user.UserName}</b>,
          </span>
        </td>
      </tr>
      <tr>
        <td style='padding:0 20px 20px 20px;'>
          <table width='100%' cellpadding='0' cellspacing='0' border='0' style='border:1px solid #e0e0e0; border-radius:6px; background:#fafafa;'>
            <tr>
              <td style='padding:15px;'>
                <span style='font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif; font-size:15px; color:#111111;'>
                  <b>{meeting.Title}</b> adlı toplantıya eklendiniz.
                </span>
                <br><br>
                <span style='font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif; font-size:14px; color:#555555;'>
                  Toplantı tarihi: <b>{meeting.ScheduledAt:dd.MM.yyyy HH:mm}</b>
                </span>
                <br><br>
                <span style='font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif; font-size:14px; color:#444444;'>
                  Detaylar için sisteme giriş yapabilirsiniz.
                </span>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td align='center' style='padding:10px 20px 0 20px;'>
          <span style='font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif; font-size:13px; color:#777777;'>
            Bu tür e-postaları <b>Profil → Bildirim Tercihleri</b> bölümünden yönetebilirsiniz.
          </span>
        </td>
      </tr>
      <tr>
        <td align='center' style='padding:20px;'>
          <hr style='border:none; border-top:1px solid #ddd; margin:0 0 10px 0; width:90%;'>
          <span style='font-family:Segoe UI,Roboto,Helvetica,Arial,sans-serif; font-size:12px; color:#999999;'>
            Bu mail otomatik olarak gönderilmiştir. Lütfen yanıtlamayın.<br/>
            © {DateTime.Now:yyyy} Meeting Management System
          </span>
        </td>
      </tr>
    </table>
  </center>
</body>
</html>";

                await _emailSender.SendAsync(user.Email, subject, body);
            }

            return new MessageResponse
            {
                Message = "Toplantıya başarıyla katıldınız!",
                Success = true
            };
        }
    }
}
