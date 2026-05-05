using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Application.Services
{
    public interface IProductService
    {
        Task<int> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
