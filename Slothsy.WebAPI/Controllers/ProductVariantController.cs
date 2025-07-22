using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ApiControllerBase
    {
        private readonly IProductVariantService _productVariantService;

        public ProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService ?? throw new ArgumentNullException(nameof(productVariantService));
        }
        /// <summary>
        /// Retrieves a product variant by its unique slug.
        /// </summary>
        /// <remarks>The slug is case-insensitive and must correspond to an existing product variant. If
        /// no matching product variant is found, the method returns a 404 Not Found response.</remarks>
        /// <param name="slug">The unique identifier for the product variant, typically a URL-friendly string.</param>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="ProductVariantDto"/> if the product variant is
        /// found; otherwise, a <see cref="NotFoundResult"/> if no product variant matches the specified slug.</returns>
        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductVariantDto>> GetBySlug(string slug)
        {
            var variant = await _productVariantService.GetBySlugAsync(slug);

            if (variant == null)
                return NotFound();

            return Ok(variant);
        }

    }
}
