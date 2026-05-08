using Vloxy.SuperaRewards.Domain.models;
using Vloxy.SuperaRewards.Domain.Repositories;

namespace Vloxy.SuperaRewards.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(
            IUserRepository userRepository,
            IProductRepository productRepository,
            ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<string> RedeemProductAsync(int userId, int productId)
        {
            // 1. Busca o aluno e o produto no banco
            var user = await _userRepository.GetByIdAsync(userId);
            var product = await _productRepository.GetByIdAsync(productId);

            // 2. Validações de Regra de Negócio
            if (user == null) return "Usuário não encontrado.";
            if (product == null) return "Produto não encontrado.";
            if (product.StockQuantity <= 0) return "Produto esgotado.";
            if (user.TotalPoints < product.PointsCost) return "Saldo de pontos insuficiente.";

            // 3. Executa a transação (Desconta os pontos e o estoque)
            user.TotalPoints -= product.PointsCost;
            product.StockQuantity -= 1;

            await _userRepository.UpdateAsync(user);
            await _productRepository.UpdateAsync(product);

            // 4. Registra no histórico para auditoria do sistema
            var transaction = new TransactionHistory
            {
                UserId = user.Id,
                Description = $"Resgate do produto: {product.Name}",
                PointsChanged = -product.PointsCost, // Fica negativo para mostrar que gastou
                TransactionDate = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);

            return "Resgate realizado com sucesso!";
        }

        public async Task<string> AddPointsAsync(int userId, int points, string description)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return "Usuário não encontrado.";

            user.TotalPoints += points;
            await _userRepository.UpdateAsync(user);

            var transaction = new TransactionHistory
            {
                UserId = user.Id,
                Description = description,
                PointsChanged = points,
                TransactionDate = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);
            return "Pontos creditados com sucesso!";
        }

        public async Task<IEnumerable<TransactionHistory>> GetUserHistoryAsync(int userId)
        {
            return await _transactionRepository.GetByUserIdAsync(userId);
        }
    }
}
