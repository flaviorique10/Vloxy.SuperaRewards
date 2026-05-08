using Microsoft.AspNetCore.Mvc;
using Vloxy.SuperaRewards.Application.Dtos;
using Vloxy.SuperaRewards.Application.Services;
using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Agora injetamos o AuthService (que tem a inteligência do BCrypt)
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = "Client",
                TotalPoints = 0
            };

            var result = await _authService.RegisterAsync(user, request.Password);

            if (result == "Usuário criado com sucesso!")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);

            if (token == null)
                return Unauthorized(new { message = "E-mail ou senha inválidos." });

            return Ok(new { token });
        }
    }                
}
