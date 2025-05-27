using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Application.Models;

namespace Slothsy.WebAPI.Controllers
{

    public class ProductController : ApiControllerBase
    {
        private readonly IProductReadService _productReadService;

        public ProductController(IProductReadService productReadService)
        {
            _productReadService = productReadService ?? throw new ArgumentNullException(nameof(productReadService));

        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="paginationParams">Pagination parameters from query string.</param>
        /// <returns>Paged result of product DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetPagedAsync([FromQuery] PaginationParams paginationParams)
        {
            var pagedProducts = await _productReadService.GetPagedAsync(paginationParams);
            return Ok(pagedProducts);
        }
        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to get.</param>
        /// <returns>Product with the specified ID, or NotFound if it does not exist.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
        {
            var product = await _productReadService.GetByIdAsync(id);

            return Ok(product);
        }

        /// <summary>
        /// Retrieves products by category with pagination support.
        /// </summary>
        /// <param name="categoryId">The category ID to filter products by.</param>
        /// <param name="paginationParams">Pagination parameters (page number & size).</param>
        /// <returns>Paged list of products for the specified category.</returns>
        [HttpGet("category/{categoryId:guid}")]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetByCategoryAsync(
            Guid categoryId,
            [FromQuery] PaginationParams paginationParams)
        {
            var pagedProducts = await _productReadService.GetByCategoryIdAsync(categoryId, paginationParams);
            return Ok(pagedProducts);
        }

        /// <summary>
        /// Searches products by name with pagination support.
        /// </summary>
        /// <param name="name">Product name or partial name to search for.</param>
        /// <param name="paginationParams">Pagination parameters (page number & size).</param>
        /// <returns>Paged list of matching products.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<ProductDto>>> SearchByNameAsync(
            [FromQuery] string name,
            [FromQuery] PaginationParams paginationParams)
        {
            var pagedProducts = await _productReadService.SearchByNameAsync(name, paginationParams);
            return Ok(pagedProducts);
        }
    }
}
