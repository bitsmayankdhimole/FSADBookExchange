using Application.UseCases.Session;
using Application.UseCases.User;
using BookExchange.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BookExchange.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;

        public SessionController(ISessionService sessionService, IUserService userService)
        {
            _sessionService = sessionService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            var sessionToken = GenerateSessionToken();
            var expirationDate = DateTime.UtcNow.AddHours(12); // Example expiration time

            await _sessionService.CreateSessionAsync(user.UserId, sessionToken, expirationDate);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expirationDate,
                Secure = true, // Ensure this is true in production
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("SessionToken", sessionToken, cookieOptions);

            return Ok(new { user.UserId, ExpirationDate = expirationDate });
        }

        [HttpGet("check-session")]
        public async Task<IActionResult> CheckSession()
        {
            if (!Request.Cookies.TryGetValue("SessionToken", out var sessionToken))
            {
                return Unauthorized();
            }

            var session = await _sessionService.GetSessionAsync(sessionToken);
            if (session == null || session.ExpirationDate < DateTime.UtcNow)
            {
                return Unauthorized();
            }

            return Ok(new { UserId = session.UserId });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> ExpireSession()
        {
            if (!Request.Cookies.TryGetValue("SessionToken", out var sessionToken))
            {
                return Ok();
            }

            await _sessionService.ExpireSessionAsync(sessionToken);
            return Ok();
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedAccess()
        {
            return Unauthorized(new { Message = "Unauthorized access. Please log in." });
        }

        [HttpGet("forbidden")]
        public IActionResult ForbiddenAccess()
        {
            return Forbid();
        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(storedSalt));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(computedHash) == storedHash;
        }

        private string GenerateSessionToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
