namespace Application.Domain.Entities
{
    public class PasswordReset
    {
        public int ResetId { get; set; }
        public int UserId { get; set; }
        public string ResetToken { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
