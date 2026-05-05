using Vloxy.SuperaRewards.Domain.models;
using Vloxy.SuperaRewards.Domain.Repositories;

namespace Vloxy.SuperaRewards.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
                
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            return await _repository.AddAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
