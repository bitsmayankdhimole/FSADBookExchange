using Application.Domain.Entities;

namespace Application.Domain.Repositories
{
    public interface IPasswordResetRepository
    {
        Task CreatePasswordResetTokenAsync(PasswordReset passwordReset);
        Task<PasswordReset?> GetPasswordResetByTokenAsync(string resetToken);
        Task MarkPasswordResetAsUsedAsync(string resetToken);
    }
}
