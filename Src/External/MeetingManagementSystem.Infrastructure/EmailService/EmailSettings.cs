namespace MeetingManagementSystem.Infrastructure.EmailService
{
    public class EmailSettings
    {
        public string From { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string SmtpServer { get; set; } = "smtp@gmail.com";
        public int Port { get; set; } = 587;
    }
}
