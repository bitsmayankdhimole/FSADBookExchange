using Application.Domain.Entities;

namespace Application.Domain.Repositories
{
    public interface ISessionRepository
    {
        Task CreateSessionAsync(Session session);
        Task<Session?> GetSessionAsync(string sessionToken);
        Task ExpireSessionAsync(string sessionToken);
    }
}
