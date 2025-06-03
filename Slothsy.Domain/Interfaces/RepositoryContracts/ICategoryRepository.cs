using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Interfaces.RepositoryContracts
{
    /// <summary>
    /// Retrieves all categories, with optional inclusion of inactive ones.
    /// </summary>
    public interface ICategoryRepository
    {

        /// <summary>
        /// Retrieves all categories, with optional inclusion of inactive ones.
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive categories.</param>
        /// <returns>List of categories.</returns>
        Task<List<Category>> GetAllCategoriesAsync(bool includeInactive = false);
        /// <summary>
        ///  Retrieves all main categories (those without a parent), optionally including inactive.
        /// </summary>
        /// <returns>List of main categories.</returns>
        Task<List<Category>> GetMainCategoriesAsync(bool includeInactive = false); // ParentCategoryId == null
        /// <summary>
        ///  Retrieves all subcategories for a given parent category, optionally including inactive.
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <returns>Collection of subcategories.</returns>
        Task<List<Category>> GetSubcategoriesAsync(Guid parentCategoryId, bool includeInactive = false);
        /// <summary>
        /// Retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Specified category</returns>
        Task<Category?> GetCategoryByIdAsync(Guid id);

        /// <summary>
        /// Retrieves the unique identifier of a category by its slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<Guid?> GetCategoryIdBySlugAsync(string slug);


        /// <summary>
        /// Adds a new category to the data store.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Newly created category</returns>
        Task<Category> AddAsync(Category category);
        /// <summary>
        /// Updates an existing category in the data store.
        /// </summary>
        /// <param name="category">Category with updated values.</param>
        /// <returns></returns>
        Task UpdateAsync(Category category);
        /// <summary>
        /// Soft deletes a category by setting its IsActive property to false.
        /// </summary>
        /// <param name="id">Unique identifier of the category to soft delete</param>
        /// <returns></returns>
        Task SoftDeleteAsync(Guid id);

        /// <summary>
        /// Hard deletes a category from the data store.
        /// </summary>
        /// <param name="id">Unique identifier of the category to hard delete</param>
        /// <returns></returns>
        Task HardDeleteAsync(Guid id);
    }
}
