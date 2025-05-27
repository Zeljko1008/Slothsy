using Slothsy.Application.DTOs;
using Slothsy.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    /// <summary>
    /// Provides read-only operations for retrieving product data.
    /// </summary>
    public interface IProductReadService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products as DTOs.</returns>
        Task<IReadOnlyList<ProductDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>A product DTO if found; otherwise, null.</returns>
        Task<ProductDto?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all products that belong to a specific category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <returns>A list of products as DTOs that belong to the specified category.</returns>
        Task<PagedResult<ProductDto>> GetByCategoryIdAsync(Guid categoryId , PaginationParams paginationParams);
        /// <summary>
        /// Searches for products by their name.
        /// </summary>
        /// <param name="name">Name or partial name of the product to search for.</param>
        /// <returns>The list of products that match the search criteria as DTOs.</returns>
        Task<PagedResult<ProductDto>> SearchByNameAsync(string name, PaginationParams paginationParams);

        /// <summary>
        ///  Gets a paged list of products based on pagination parameters.
        /// </summary>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        Task<PagedResult<ProductDto>> GetPagedAsync(PaginationParams paginationParams);

    }
}
