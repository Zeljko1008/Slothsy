using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Application.Models;
using Slothsy.Common.Pagination;

namespace Slothsy.WebAPI.Controllers
{

    public class ProductsController : ApiControllerBase
    {
        private readonly IProductReadService _productReadService;

        public ProductsController(IProductReadService productReadService)
        {
            _productReadService = productReadService ?? throw new ArgumentNullException(nameof(productReadService));

        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="paginationParams">Pagination parameters from query string.</param>
        /// <returns>Paged result of product DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetAllAsync([FromQuery] PaginationParams paginationParams)
        {
            var pagedProducts = await _productReadService.GetAllAsync(paginationParams);
            return Ok(pagedProducts);
        }

        /// <summary>
        /// Gets products by slug (unique identifier).
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductDto>> GetProductBySlugAsync(string slug)
        {
            var product = await _productReadService.GetProductBySlugAsync(slug,includeInactive:false);

            return Ok(product);
        }
       


        /// <summary>
        /// Retrieves products that belong to a specific category by its slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        [HttpGet("categorytree/{slug}")]
public async Task<ActionResult<PagedResult<ProductDto>>> GetByCategoryTreeSlugAsync(
    string slug,
    [FromQuery] PaginationParams paginationParams)
{
    var result = await _productReadService.GetByCategoryTreeSlugAsync(slug, paginationParams);
    return Ok(result);
}

        /// <summary>
        /// Searches products by name with pagination support.
        /// </summary>
        /// <param name="name">Product name or partial name to search for.</param>
        /// <param name="paginationParams">Pagination parameters (page number & size).</param>
        /// <returns>Paged list of matching products.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<ProductDto>>> SearchByNameAsync([FromQuery] string name, [FromQuery] PaginationParams paginationParams)
        {
            var result = await _productReadService.SearchByNameAsync(name, paginationParams);
            return Ok(result);
        }

    }
}
