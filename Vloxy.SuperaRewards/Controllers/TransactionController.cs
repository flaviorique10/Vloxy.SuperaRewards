using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vloxy.SuperaRewards.Application.Dtos;
using Vloxy.SuperaRewards.Application.Services;

namespace Vloxy.SuperaRewards.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("redeem/{productId}")]
        public async Task<IActionResult> Redeem(int productId)
        {
            // Puxa o ID do usuário que está escondido dentro do Token JWT (Claim)
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(new { message = "Token inválido ou usuário não identificado." });
            }

            // Chama a regra de negócio
            var result = await _transactionService.RedeemProductAsync(userId, productId);

            if (result == "Resgate realizado com sucesso!")
            {
                return Ok(new { message = result });
            }

            // Retorna 400 Bad Request se deu erro de saldo, estoque ou não encontrou
            return BadRequest(new { message = result });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            // Descobre quem é o usuário logado pelo Token
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(new { message = "Token inválido ou usuário não identificado." });
            }

            // Busca as transações só dele
            var history = await _transactionService.GetUserHistoryAsync(userId);

            return Ok(history);
        }

        [HttpPost("add-points")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPoints([FromBody] AddPointsRequest request)
        {
            var result = await _transactionService.AddPointsAsync(request.TargetUserId, request.Points, request.Reason);

            if (result == "Pontos creditados com sucesso!")
            {
                return Ok(new { message = result });
            }

            return BadRequest(new { message = result });
        }
    }
}
