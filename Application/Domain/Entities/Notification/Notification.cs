namespace Application.Domain.Entities.Notification
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSent { get; set; }
    }
}
