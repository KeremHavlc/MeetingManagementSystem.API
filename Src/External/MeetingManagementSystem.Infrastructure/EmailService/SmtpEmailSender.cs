using MailKit.Net.Smtp;
using MailKit.Security;
using MeetingManagementSystem.Application.Abstractions;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace MeetingManagementSystem.Infrastructure.EmailService
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public SmtpEmailSender(IOptions<EmailSettings> settings)
        {
            _emailSettings = settings.Value;
        }

        public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls, ct);
            await smtp.AuthenticateAsync(_emailSettings.From, _emailSettings.Password, ct);
            await smtp.SendAsync(email, ct);
            await smtp.DisconnectAsync(true, ct);
        }
    }
}
