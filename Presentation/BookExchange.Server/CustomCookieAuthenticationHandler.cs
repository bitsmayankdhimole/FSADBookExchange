using Application.UseCases.Session;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BookExchange.Server
{
    public class CustomCookieAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ISessionService _sessionService;

        public CustomCookieAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ISessionService sessionService)
            : base(options, logger, encoder, clock)
        {
            _sessionService = sessionService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Cookies.TryGetValue("SessionToken", out var sessionToken))
            {
                return AuthenticateResult.Fail("No session token found.");
            }

            var session = await _sessionService.GetSessionAsync(sessionToken);
            if (session == null || session.ExpirationDate < DateTime.UtcNow)
            {
                return AuthenticateResult.Fail("Invalid or expired session token.");
            }

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, session.UserId.ToString()) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
