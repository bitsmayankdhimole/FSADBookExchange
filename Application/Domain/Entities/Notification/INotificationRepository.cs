namespace Application.Domain.Entities.Notification
{
    public interface INotificationRepository
    {
        Task SendEmailNotificationAsync(string toEmail, string subject, string body);
    }
}
