using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(TransactionHistory transaction);
        Task<IEnumerable<TransactionHistory>> GetByUserIdAsync(int userId);
    }
}
