namespace Application.UseCases.Notification
{
    public interface INotificationService
    {
        Task SendEmailNotificationAsync(string toEmail, string subject, string body);
    }
}
