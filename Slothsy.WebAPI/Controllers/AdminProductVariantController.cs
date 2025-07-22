using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/admin/products/{productColorVariantId}/variants")]
    [ApiController]
    public class AdminProductVariantController : ControllerBase
    {
        private readonly IAdminProductVariantService _adminProductVariantService;

        public AdminProductVariantController(IAdminProductVariantService adminProductVariantService)
        {
            _adminProductVariantService = adminProductVariantService ?? throw new ArgumentNullException(nameof(adminProductVariantService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVariantDto>>> GetAllAsync(Guid productColorVariantId)
        {
            var variants = await _adminProductVariantService.GettAllAsync(productColorVariantId);
            if (variants == null || !variants.Any())
                return NotFound("No variants found for this color variant.");
            return Ok(variants);
        }

        // GET: api/admin/products/{productColorVariantId}/variants/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVariantDto>> GetById(Guid productColorVariantId, Guid id)
        {


            var variant = await _adminProductVariantService.GetProductVariantByIdAsync(id);
            if (variant == null)
                return NotFound();

            if (variant.ProductColorVariantId != productColorVariantId)
                return BadRequest("Variant does not belong to the specified color variant.");

            return Ok(variant);
        }

        // POST: api/admin/products/{productColorVariantId}/variants
        [HttpPost]
        public async Task<IActionResult> Create(Guid productColorVariantId, [FromBody] CreateProductVariantDto dto)
        {
            if (dto.ProductColorVariantId != productColorVariantId)
                return BadRequest("ProductColorVariantId mismatch between route and body.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _adminProductVariantService.AddProductVariantAsync(dto);
            return CreatedAtAction(nameof(GetById), new { productColorVariantId, id = newId }, null);
        }
    }
}
