using Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BookExchange.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            if (!IsValidEmail(email))
            {
                return BadRequest("Invalid email format.");
            }

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                await _userService.RegisterUserAsync(request.Email, request.Password, request.FirstName, request.LastName);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            // Implement logic to update user by id
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // Implement logic to delete user by id
            return Ok();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
                return emailRegex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class CreateUserRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class UpdateUserRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
