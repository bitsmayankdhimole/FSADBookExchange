using Entities = Application.Domain.Entities;
namespace Application.UseCases.User
{
    public interface IUserService
    {
        Task RegisterUserAsync(string email, string password, string? firstName, string? lastName);
        Task<Entities.User?> GetUserByEmailAsync(string email);
    }
}
