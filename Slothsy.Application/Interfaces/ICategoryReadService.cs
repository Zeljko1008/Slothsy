using Slothsy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    /// <summary>
    /// Service interface for reading category data.
    /// </summary>
    public interface ICategoryReadService
    {
        /// <summary>
        /// Retrieves all categories, optionally including inactive ones.
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        Task<List<CategoryDto>> GetAllCategoriesAsync(bool includeInactive = false);

        /// <summary>
        /// Retrieves all main categories (those without a parent category), optionally including inactive ones.
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        Task<List<CategoryDto>> GetMainCategoriesAsync(bool includeInactive = false, string? type = null);

        /// <summary>
        /// Retrieves all subcategories for a given parent category, optionally including inactive ones.
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        Task<List<CategoryDto>> GetSubCategoriesAsync(Guid parentCategoryId, bool includeInactive = false);

        /// <summary>
        /// Retrieves a category by its unique identifier, optionally including inactive ones.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        Task<CategoryDto?> GetCategoryByIdAsync(Guid id, bool includeInactive = false);

        /// <summary>
        /// Retrieves a category by its unique slug, optionally including inactive ones.
        /// </summary>
        /// <param name="slug">SEO-friendly identifier of the category.</param>
        /// <param name="includeInactive">Whether to include inactive categories in the result.</param>
        /// <returns>A single category DTO or null if not found.</returns>
        Task<CategoryDto?> GetCategoryBySlugAsync(string slug, bool includeInactive = false);

        /// <summary>
        /// Retrieves all subcategories for a given parent category identified by slug.
        /// </summary>
        /// <param name="parentCategorySlug">Slug of the parent category.</param>
        /// <param name="includeInactive">Whether to include inactive categories in the result.</param>
        /// <returns>List of subcategory DTOs.</returns>
        Task<List<CategoryDto>> GetSubCategoriesBySlugAsync(string parentCategorySlug, bool includeInactive = false);

    }
}
