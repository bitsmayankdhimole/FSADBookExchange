using Entities = Application.Domain.Entities;
using Application.Domain.Repositories;

namespace Application.UseCases.Session
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task CreateSessionAsync(int userId, string sessionToken, DateTime expirationDate)
        {
            var session = new Entities.Session
            {
                UserId = userId,
                SessionToken = sessionToken,
                CreatedDate = DateTime.UtcNow,
                ExpirationDate = expirationDate
            };

            await _sessionRepository.CreateSessionAsync(session);
        }

        public async Task<Entities.Session?> GetSessionAsync(string sessionToken)
        {
            return await _sessionRepository.GetSessionAsync(sessionToken);
        }

        public async Task ExpireSessionAsync(string sessionToken)
        {
            await _sessionRepository.ExpireSessionAsync(sessionToken);
        }
    }
}
