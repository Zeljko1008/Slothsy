using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorVariantController : ControllerBase
    {
        private readonly IProductColorVariantService _productColorVariantService;

        public ProductColorVariantController(IProductColorVariantService productColorVariantService)
        {
            _productColorVariantService = productColorVariantService ?? throw new ArgumentNullException(nameof(productColorVariantService));
        }

        /// <summary>
        /// Get a product color variant by its slug.
        /// </summary>
        /// <param name="slug">The slug of the product color variant.</param>
        /// <returns>ProductColorVariantDto</returns>
        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductColorVariantDto>> GetBySlug(string slug)
        {
            var result = await _productColorVariantService.GetBySlugAsync(slug);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
