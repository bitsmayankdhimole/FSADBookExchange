using System.Security.Cryptography;
using System.Text;
using Application.Domain.Repositories;
using Entities = Application.Domain.Entities;

namespace Application.UseCases.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUserAsync(string email, string password, string? firstName, string? lastName)
        {
            // Hash password and generate salt (implementation not shown)
            var passwordHash = HashPassword(password, out var passwordSalt);

            var user = new Entities.User
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = firstName,
                LastName = lastName,
                CreatedDate = DateTime.UtcNow
            };

            await _userRepository.RegisterUserAsync(user);
        }

        public async Task<Entities.User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
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
