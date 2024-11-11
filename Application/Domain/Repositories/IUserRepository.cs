using Application.Domain.Entities;

namespace Application.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task RegisterUserAsync(User user);
    }
}
