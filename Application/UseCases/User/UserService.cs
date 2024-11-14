using System.Security.Cryptography;
using System.Text;
using Application.Domain.Entities.User;
using Application.UseCases.Notification;
using UserEntity = Application.Domain.Entities.User;

namespace Application.UseCases.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;

        public UserService(IUserRepository userRepository, INotificationService notificationService)
        {
            _userRepository = userRepository;
            _notificationService = notificationService;
        }

        public async Task RegisterUserAsync(string email, string password, string? firstName, string? lastName)
        {
            var passwordHash = HashPassword(password, out var passwordSalt);

            var user = new UserEntity.User
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = firstName,
                LastName = lastName,
                CreatedDate = DateTime.UtcNow
            };

            await _userRepository.RegisterUserAsync(user);
            await _notificationService.SendEmailNotificationAsync(email, "Welcome to our service!", "Thank you for registering.");
        }

        public async Task<UserEntity.User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task CreatePasswordResetTokenAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var expirationDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow.AddHours(1), "India Standard Time");

            await _userRepository.CreatePasswordResetTokenAsync(user.UserId, resetToken, expirationDate);

            await _notificationService.SendEmailNotificationAsync(email, "Password Reset", $"Your password reset token is: {resetToken}");
        }

        public async Task ResetPasswordAsync(string resetToken, string newPassword)
        {
            var user = await _userRepository.GetUserByResetTokenAsync(resetToken);
            if (user == null)
            {
                throw new Exception("Invalid or expired reset token.");
            }

            var newPasswordHash = HashPassword(newPassword, out var newPasswordSalt);
            await _userRepository.ResetPasswordAsync(resetToken, newPasswordHash, newPasswordSalt);

            await _notificationService.SendEmailNotificationAsync(user.Email, "Password Reset Successful", "Your password has been successfully reset.");
        }

        private string HashPassword(string password, out string salt)
        {
            using var hmac = new HMACSHA256();
            var saltkey = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            salt = Convert.ToBase64String(saltkey);
            return Convert.ToBase64String(passwordHash);
        }
    }
}
