using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Exceptions;
using Slothsy.Application.Interfaces;

namespace Slothsy.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing product categories.
    /// </summary>

    public class CategoriesController :ApiControllerBase
    {
        private readonly ICategoryReadService _categoryReadService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryReadService categoryReadService, ILogger<CategoriesController> logger)
        {
            _categoryReadService = categoryReadService ?? throw new ArgumentNullException(nameof(categoryReadService));
            _logger = logger;
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
        public async Task<ActionResult<List<CategoryDto>>> GetMainCategories(
     [FromQuery] bool includeInactive = false,
     [FromQuery] string? type = null)
        {
            var categories = await _categoryReadService.GetMainCategoriesAsync(includeInactive, type);
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

            if (categories == null)
            {
                _logger.LogWarning("No subcategories found for parent category ID {ParentId}", parentCategoryId);
                return Ok(new List<CategoryDto>());
            }

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

        /// <summary>
        /// Returns a category by slug.
        /// </summary>
        /// <param name="slug">Category slug.</param>
        /// <param name="includeInactive">Whether to return an inactive category.</param>
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryBySlug(string slug, [FromQuery] bool includeInactive = false)
        {
            var category = await _categoryReadService.GetCategoryBySlugAsync(slug, includeInactive);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Returns subcategories of a category identified by slug.
        /// </summary>
        /// <param name="parentCategorySlug">Slug of the parent category.</param>
        /// <param name="includeInactive">Whether to include inactive subcategories.</param>
        [HttpGet("slug/{parentCategorySlug}/subcategories")]
        public async Task<ActionResult<List<CategoryDto>>> GetSubCategoriesBySlug(string parentCategorySlug, [FromQuery] bool includeInactive = false)
        {
            try
            {
                var subCategories = await _categoryReadService.GetSubCategoriesBySlugAsync(parentCategorySlug, includeInactive);
                return Ok(subCategories);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Parent category not found for slug: {Slug}", parentCategorySlug);
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
