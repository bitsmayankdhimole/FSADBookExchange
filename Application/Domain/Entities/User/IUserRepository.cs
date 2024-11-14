namespace Application.Domain.Entities.User
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task RegisterUserAsync(User user);
        Task CreatePasswordResetTokenAsync(int userId, string resetToken, DateTime expirationDate);
        Task<User?> GetUserByResetTokenAsync(string resetToken);
        Task ResetPasswordAsync(string resetToken, string newPasswordHash, string newPasswordSalt);
    }
}
