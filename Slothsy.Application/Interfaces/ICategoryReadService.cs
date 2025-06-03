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
        Task<List<CategoryDto>> GetMainCategoriesAsync(bool includeInactive = false);

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
    }
}
