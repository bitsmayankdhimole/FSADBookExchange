using Application.Domain.Entities;
using Application.Domain.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess.Repositories
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly string _connectionString;

        public PasswordResetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreatePasswordResetTokenAsync(PasswordReset passwordReset)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "CreatePasswordResetToken",
                new
                {
                    passwordReset.UserId,
                    passwordReset.ResetToken,
                    passwordReset.ExpirationDate
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<PasswordReset?> GetPasswordResetByTokenAsync(string resetToken)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<PasswordReset>(
                "SELECT * FROM PasswordResets WHERE ResetToken = @ResetToken",
                new { ResetToken = resetToken });
        }

        public async Task MarkPasswordResetAsUsedAsync(string resetToken)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "UPDATE PasswordResets SET IsUsed = 1 WHERE ResetToken = @ResetToken",
                new { ResetToken = resetToken });
        }
    }
}
