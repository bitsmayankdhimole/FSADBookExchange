using System.Net;
using System.Net.Mail;
using Application.Domain.Entities.Notification;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly string _smtpServer;
        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly int _smtpPort;

        public NotificationRepository(IConfiguration configuration)
        {
            _fromEmail = configuration["EmailSettings:FromEmail"];
            _fromPassword = configuration["EmailSettings:FromPassword"];
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _apiKey = configuration["EmailSettings:ApiKey"];
            _apiSecret = configuration["EmailSettings:ApiSecret"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
        }

        public async Task SendEmailNotificationAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_apiKey, _apiSecret),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
