namespace Application.Domain.Entities.Session
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string SessionToken { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
