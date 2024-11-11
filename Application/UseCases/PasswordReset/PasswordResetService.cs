using Entities = Application.Domain.Entities;
using Application.Domain.Repositories;

namespace Application.UseCases.PasswordReset
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IPasswordResetRepository _passwordResetRepository;
        private readonly IUserRepository _userRepository;

        public PasswordResetService(IPasswordResetRepository passwordResetRepository, IUserRepository userRepository)
        {
            _passwordResetRepository = passwordResetRepository;
            _userRepository = userRepository;
        }

        public async Task CreatePasswordResetTokenAsync(int userId, string resetToken, DateTime expirationDate)
        {
            var passwordReset = new Entities.PasswordReset
            {
                UserId = userId,
                ResetToken = resetToken,
                ExpirationDate = expirationDate,
                IsUsed = false
            };

            await _passwordResetRepository.CreatePasswordResetTokenAsync(passwordReset);
        }

        public async Task ResetPasswordAsync(string resetToken, string newPasswordHash, string newPasswordSalt)
        {
            var passwordReset = await _passwordResetRepository.GetPasswordResetByTokenAsync(resetToken);

            if (passwordReset == null || passwordReset.IsUsed || passwordReset.ExpirationDate <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("Invalid or expired reset token.");
            }

            var user = await _userRepository.GetUserByEmailAsync(passwordReset.UserId.ToString());

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;

            await _userRepository.RegisterUserAsync(user);
            await _passwordResetRepository.MarkPasswordResetAsUsedAsync(resetToken);
        }
    }
}
