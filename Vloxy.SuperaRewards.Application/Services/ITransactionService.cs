using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Application.Services
{
    public interface ITransactionService
    {
        Task<string> RedeemProductAsync(int userId, int productId);
        Task<string> AddPointsAsync(int userId, int points, string description);
        Task<IEnumerable<TransactionHistory>> GetUserHistoryAsync(int userId);
    }
}
