using Vloxy.SuperaRewards.Domain.models;
using Vloxy.SuperaRewards.Domain.Repositories;

namespace Vloxy.SuperaRewards.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            // Verifica se o usuário existe e compara a senha digitada com o Hash salvo no banco
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return _tokenService.GenerateToken(user);
        }

        public async Task<string> RegisterAsync(User user, string password)
        {
            var exists = await _userRepository.GetByEmailAsync(user.Email);
            if (exists != null) return "E-mail já cadastrado.";

            // Transforma a senha em texto puro num Hash irreversível antes de salvar
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            await _userRepository.AddAsync(user);

            return "Usuário criado com sucesso!";
        }
    }
}
