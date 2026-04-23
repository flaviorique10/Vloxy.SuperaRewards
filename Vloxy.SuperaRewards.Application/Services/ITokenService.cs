using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
