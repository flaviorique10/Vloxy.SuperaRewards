using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
