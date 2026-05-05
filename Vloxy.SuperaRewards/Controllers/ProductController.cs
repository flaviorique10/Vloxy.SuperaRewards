using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vloxy.SuperaRewards.Application.Services;
using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        // Injeção de Dependência do Serviço
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // O [Authorize] garante que só quem tem a Role "Admin" no token consegue acessar
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var id = await _productService.CreateProductAsync(product);

            // Retorna um status 201 (Created) apontando onde o item recém-criado pode ser encontrado
            return CreatedAtAction(nameof(GetAll), new { id }, product);
        }

        // O [AllowAnonymous] deixa a rota pública, assim qualquer aluno pode ver a vitrine
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
