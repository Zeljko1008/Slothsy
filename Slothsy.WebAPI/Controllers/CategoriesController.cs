using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;

namespace Slothsy.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing product categories.
    /// </summary>

    public class CategoriesController :ApiControllerBase
    {
        private readonly ICategoryReadService _categoryReadService;

        public CategoriesController(ICategoryReadService categoryReadService)
        {
            _categoryReadService = categoryReadService ?? throw new ArgumentNullException(nameof(categoryReadService));
        }


        /// <summary>
        /// Returns all categories.
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive categories.</param>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategoriesAsync([FromQuery] bool includeInactive = false)
        {
            var categories = await _categoryReadService.GetAllCategoriesAsync(includeInactive);
            return Ok(categories);
        }

        /// <summary>
        /// Returns all main (top-level) categories.
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive categories.</param>
        [HttpGet("main")]
        public async Task<ActionResult<List<CategoryDto>>> GetMainCategories([FromQuery] bool includeInactive = false)
        {
            var categories = await _categoryReadService.GetMainCategoriesAsync(includeInactive);
            return Ok(categories);
        }

        /// <summary>
        /// Returns subcategories of the given parent category.
        /// </summary>
        /// <param name="parentCategoryId">Parent category ID.</param>
        /// <param name="includeInactive">Whether to include inactive subcategories.</param>
        [HttpGet("{parentCategoryId:guid}/subcategories")]
        public async Task<ActionResult<List<CategoryDto>>> GetSubcategories(Guid parentCategoryId, [FromQuery] bool includeInactive = false)
        {
            var categories = await _categoryReadService.GetSubCategoriesAsync(parentCategoryId, includeInactive);
            return Ok(categories);
        }

        /// <summary>
        /// Returns a category by ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <param name="includeInactive">Whether to return an inactive category.</param>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id, [FromQuery] bool includeInactive = false)
        {
            var categories = await _categoryReadService.GetCategoryByIdAsync(id, includeInactive);
            if (categories == null)
                return NotFound();

            return Ok(categories);
        }
    }
}
