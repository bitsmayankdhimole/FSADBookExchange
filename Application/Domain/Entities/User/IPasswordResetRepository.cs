namespace Application.Domain.Entities.User
{
    public interface IPasswordResetRepository
    {
        Task CreatePasswordResetTokenAsync(PasswordReset passwordReset);
        Task<PasswordReset?> GetPasswordResetByTokenAsync(string resetToken);
        Task MarkPasswordResetAsUsedAsync(string resetToken);
    }
}
