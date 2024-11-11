namespace BookExchange.Server.Models
{
    public class CreateSessionRequest
    {
        public int UserId { get; set; }
        public string SessionToken { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
    }
}
