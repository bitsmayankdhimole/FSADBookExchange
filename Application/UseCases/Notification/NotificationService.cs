using Application.Domain.Entities.Notification;

namespace Application.UseCases.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task SendEmailNotificationAsync(string toEmail, string subject, string body)
        {
            await _notificationRepository.SendEmailNotificationAsync(toEmail, subject, body);
        }
    }
}
