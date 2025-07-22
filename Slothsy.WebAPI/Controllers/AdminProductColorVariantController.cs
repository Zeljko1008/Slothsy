using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Domain.Entities;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/admin/products/{productId}/color-variants")]
    [ApiController]
    public class AdminProductColorVariantController : ControllerBase
    {

        private readonly IAdminProductColorVariantService _adminProductColorVariantService;

        public AdminProductColorVariantController(IAdminProductColorVariantService adminProductColorVariantService)
        {
            _adminProductColorVariantService = adminProductColorVariantService ?? throw new ArgumentNullException(nameof(adminProductColorVariantService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductColorVariantDto>>> GetAllAsync(Guid productId)
        {
            var productColorVariants = await _adminProductColorVariantService.GetAllAsync(productId);

            if (productColorVariants == null || !productColorVariants.Any())
                return NotFound("No color variants found for this product.");

            return Ok(productColorVariants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductColorVariantDto>> GetById(Guid productId, Guid id)
        {
            var variant = await _adminProductColorVariantService.GetColorVariantByIdAsync(id);
            if (variant == null)
                return NotFound();

          
            if (variant.ProductId != productId)
                return BadRequest("Color variant does not belong to the specified product.");

            return Ok(variant);
        }


        [HttpPost]
        public async Task<IActionResult> AddColorVariant(Guid productId,[FromBody] CreateProductColorVariantDto dto)
        {
            if (productId != dto.ProductId)
                return BadRequest("Product ID in URL does not match product ID in body.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newColorVariantId = await _adminProductColorVariantService.AddColorVariantAsync(dto);
            return CreatedAtAction(nameof(GetById), new { productId = dto.ProductId, id = newColorVariantId }, null);
        }
    }
}
