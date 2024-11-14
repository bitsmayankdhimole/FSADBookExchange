namespace Application.Domain.Entities.Session
{
    public interface ISessionRepository
    {
        Task CreateSessionAsync(Session session);
        Task<Session?> GetSessionAsync(string sessionToken);
        Task ExpireSessionAsync(string sessionToken);
    }
}
