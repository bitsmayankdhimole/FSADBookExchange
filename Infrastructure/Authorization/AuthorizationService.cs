using Application.UseCases.Session;

namespace Infrastructure.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISessionService _sessionService;

        public AuthorizationService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<bool> ValidateSessionTokenAsync(string sessionToken)
        {
            var session = await _sessionService.GetSessionAsync(sessionToken);
            return session != null && session.ExpirationDate >= DateTime.UtcNow;
        }
    }
}
