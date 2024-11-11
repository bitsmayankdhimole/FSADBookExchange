using Application.Domain.Entities;
using Application.Domain.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly string _connectionString;

        public SessionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
        }

        public async Task CreateSessionAsync(Session session)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "CreateSession",
                new
                {
                    session.UserId,
                    session.SessionToken,
                    session.ExpirationDate
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Session?> GetSessionAsync(string sessionToken)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Session>(
                "SELECT * FROM Sessions WHERE SessionToken = @SessionToken",
                new { SessionToken = sessionToken });
        }

        public async Task ExpireSessionAsync(string sessionToken)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "UPDATE Sessions SET ExpirationDate = GETDATE() WHERE SessionToken = @SessionToken",
                new { SessionToken = sessionToken });
        }
    }
}
