using Entities = Application.Domain.Entities;

namespace Application.UseCases.Session
{
    public interface ISessionService
    {
        Task CreateSessionAsync(int userId, string sessionToken, DateTime expirationDate);
        Task<Entities.Session?> GetSessionAsync(string sessionToken);
        Task ExpireSessionAsync(string sessionToken);
    }
}
