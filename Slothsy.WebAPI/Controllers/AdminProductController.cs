using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Common.Helpers;
using Slothsy.Common.Pagination;
using Slothsy.Domain.Enums;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/admin/products")]
    [ApiController]
    public class AdminProductController : ControllerBase
    {
        private readonly IAdminProductService _adminProductService;

        public AdminProductController(IAdminProductService adminProductService)
        {
            _adminProductService = adminProductService ?? throw new ArgumentNullException(nameof(adminProductService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var products = await _adminProductService.GetAllAsync(paginationParams);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var productDto = await _adminProductService.GetProductById(id, includeInactive: true);
            if (productDto == null) return NotFound();

            return Ok(productDto);
        }



        [HttpPost]

        public async Task<ActionResult<Guid>> CreateProductAsync([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productId = await _adminProductService.AddProductAsync(createProductDto);

            return CreatedAtAction(nameof(GetById), new { id = productId }, new { id = productId });
        }

        
    }
}
