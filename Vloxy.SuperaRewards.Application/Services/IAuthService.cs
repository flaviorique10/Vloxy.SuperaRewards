using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Application.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<string> RegisterAsync(User user, string password);
    }
}
