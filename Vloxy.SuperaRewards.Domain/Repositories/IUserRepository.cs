using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task UpdateAsync(User user);
    }
}
