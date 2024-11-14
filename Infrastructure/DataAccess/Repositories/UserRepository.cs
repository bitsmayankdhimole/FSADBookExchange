using Dapper;
using Application.Domain.Entities.User;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<User>(
                "GetUser",
                new { Email = email },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task RegisterUserAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "RegisterUser",
                new
                {
                    user.Email,
                    user.PasswordHash,
                    user.PasswordSalt,
                    user.FirstName,
                    user.LastName
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task CreatePasswordResetTokenAsync(int userId, string resetToken, DateTime expirationDate)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "CreatePasswordResetToken",
                new
                {
                    UserId = userId,
                    ResetToken = resetToken,
                    ExpirationDate = expirationDate
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<User?> GetUserByResetTokenAsync(string resetToken)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<User>(
                "GetUserByResetToken",
                new { ResetToken = resetToken },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task ResetPasswordAsync(string resetToken, string newPasswordHash, string newPasswordSalt)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "ResetPassword",
                new
                {
                    ResetToken = resetToken,
                    NewPasswordHash = newPasswordHash,
                    NewPasswordSalt = newPasswordSalt
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
