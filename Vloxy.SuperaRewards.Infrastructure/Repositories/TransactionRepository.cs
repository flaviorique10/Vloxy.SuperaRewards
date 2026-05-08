using Vloxy.SuperaRewards.Domain.models;
using Vloxy.SuperaRewards.Domain.Repositories;
using Vloxy.SuperaRewards.Infrastructure.Infrastructure;

namespace Vloxy.SuperaRewards.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TransactionHistory transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
