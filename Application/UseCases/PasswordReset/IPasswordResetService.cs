namespace Application.UseCases.PasswordReset
{
    public interface IPasswordResetService
    {
        Task CreatePasswordResetTokenAsync(int userId, string resetToken, DateTime expirationDate);
        Task ResetPasswordAsync(string resetToken, string newPasswordHash, string newPasswordSalt);
    }
}
