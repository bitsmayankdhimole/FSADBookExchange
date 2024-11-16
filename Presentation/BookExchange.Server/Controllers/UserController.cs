using Application.UseCases.User;
using BookExchange.Server.Models;
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


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                if (!IsValidEmail(request.Email))
                {
                    return BadRequest("Invalid email format.");
                }

                if (!IsValidPassword(request.Password))
                {
                    return BadRequest("Invalid password format.");
                }
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
            // TODO: Implement update user by id
            return Ok();
        }


        [HttpPost("password-reset-token")]
        public async Task<IActionResult> CreatePasswordResetToken([FromBody] PasswordResetRequest request)
        {
            try
            {
                if (!IsValidEmail(request.Email))
                {
                    return BadRequest("Invalid email format.");
                }

                await _userService.CreatePasswordResetTokenAsync(request.Email);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                if (!IsValidPassword(request.NewPassword))
                {
                    return BadRequest("Invalid password format.");
                }

                await _userService.ResetPasswordAsync(request.ResetToken, request.NewPassword);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
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

        private bool IsValidPassword(string password)
        {
            const int MinLength = 8;
            string SpecialCharacters = @"[!@#$%^&*(),.?""{}|<>]";

            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < MinLength)
                return false;

            if (!Regex.IsMatch(password, SpecialCharacters))
                return false;

            return true;
        }
    }

}
