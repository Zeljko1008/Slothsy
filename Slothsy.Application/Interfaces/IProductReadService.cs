using Slothsy.Application.DTOs;
using Slothsy.Application.Models;
using Slothsy.Common.Pagination;
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
        Task<PagedResult<ProductDto>> GetAllAsync(PaginationParams pagination);

        /// <summary>
        /// Retrieves a product by its slug (unique identifier).
        /// </summary>
        /// <param name="slug">a unique identifier for the product, typically a URL-friendly string.</param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        Task<ProductDto?> GetProductBySlugAsync(string slug, bool includeInactive);

        /// <summary>
        /// Retrieves products that belong to a specific category by its slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        Task<PagedResult<ProductDto>> GetByCategorySlugAsync(string slug, PaginationParams paginationParams);
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
        //Task<PagedResult<ProductDto>> GetPagedAsync(PaginationParams paginationParams);

    }
}
