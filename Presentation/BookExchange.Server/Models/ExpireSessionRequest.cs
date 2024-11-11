namespace BookExchange.Server.Models
{
    public class ExpireSessionRequest
    {
        public string SessionToken { get; set; } = string.Empty;
    }
}
