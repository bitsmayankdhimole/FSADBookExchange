using Application.Domain.Entities;
using Application.Domain.Repositories;
using Dapper;
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
    }
}
