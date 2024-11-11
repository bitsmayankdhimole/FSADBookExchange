using Application.UseCases.Session;
using BookExchange.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookExchange.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("create-session")]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequest request)
        {
            await _sessionService.CreateSessionAsync(request.UserId, request.SessionToken, request.ExpirationDate);
            return Ok();
        }

        [HttpGet("get-session/{sessionToken}")]
        public async Task<IActionResult> GetSession(string sessionToken)
        {
            var session = await _sessionService.GetSessionAsync(sessionToken);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpPost("expire-session")]
        public async Task<IActionResult> ExpireSession([FromBody] ExpireSessionRequest request)
        {
            await _sessionService.ExpireSessionAsync(request.SessionToken);
            return Ok();
        }
    }
    
}
