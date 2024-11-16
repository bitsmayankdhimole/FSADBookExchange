namespace Application.UseCases.Session
{
    public interface IAuthorizationService
    {
        Task<bool> ValidateSessionTokenAsync(string sessionToken);
    }
}
