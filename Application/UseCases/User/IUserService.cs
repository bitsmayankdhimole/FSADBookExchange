using Entities = Application.Domain.Entities;
namespace Application.UseCases.User
{
    public interface IUserService
    {
        Task RegisterUserAsync(string email, string password, string? firstName, string? lastName);
        Task<Entities.User.User?> GetUserByEmailAsync(string email);
        Task CreatePasswordResetTokenAsync(string email);
        Task ResetPasswordAsync(string resetToken, string newPassword);
    }
}
