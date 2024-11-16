using Application.UseCases.Session;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Authorization
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public AuthorizationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var authorizeAttribute = endpoint.Metadata.GetMetadata<AuthorizeAttribute>();
                if (authorizeAttribute != null)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
                        if (context.Request.Cookies.TryGetValue("SessionToken", out var sessionToken))
                        {
                            var session = await sessionService.GetSessionAsync(sessionToken);
                            if (session != null && session.ExpirationDate >= DateTime.UtcNow)
                            {
                                context.Items["UserId"] = session.UserId;
                            }
                            else
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                return;
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
