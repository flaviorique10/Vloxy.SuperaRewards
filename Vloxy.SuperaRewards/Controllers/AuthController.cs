using Microsoft.AspNetCore.Mvc;
using Vloxy.SuperaRewards.Application.Services;
using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login-teste")]
        public IActionResult LoginTeste()
        {
            // Simulando um usuário "Admin" (Poderia ser o seu sogro)
            var usuarioSimulado = new User
            {
                Id = 1,
                Name = "Sogro Admin",
                Email = "admin@supera.com",
                Role = "Admin",
                TotalPoints = 0
            };

            // Chama a fábrica de tokens
            var token = _tokenService.GenerateToken(usuarioSimulado);

            return Ok(new { MeuToken = token });
        }
    }
}
