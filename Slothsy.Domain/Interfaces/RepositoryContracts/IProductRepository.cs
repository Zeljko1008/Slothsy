using Slothsy.Common.Pagination;
using Slothsy.Domain.Entities;

namespace Slothsy.Domain.Interfaces.RepositoryContracts
{
    /// <summary>
    /// Contract for data access operations related to products.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        Task<PagedResult<Product>> GetAllAsync(PaginationParams paginationParams);


        /// <summary>
        /// Retrieves a product by its slug (unique identifier).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        Task<Product?> GetByIdAsync(Guid id, bool includeInactive);

        /// <summary>
        /// Retrieves all products that belong to a specific category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <returns>A list of products in the specified category.</returns>
        Task<PagedResult<Product>> GetByCategoryIdAsync(Guid categoryId, PaginationParams paginationParams);

        /// <summary>
        /// Searches for products by their name.
        /// </summary>
        /// <param name="name">name or partial name of the product to search for.</param>
        /// <returns>The list of products that match the search criteria.</returns>
        Task<List<Product>> SearchByNameAsync(string name);

        /// <summary>
        /// Adds a new product to the data store.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The created product.</returns>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Soft deletes a product by setting its IsActive property to false.
        /// </summary>
        /// <param name="id">The unique identifier of the product to soft delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SoftDeleteAsync(Guid id);

        /// <summary>
        /// Hard deletes a product from the data store.
        /// </summary>
        /// <param name="id">Unique identifier of the product to hard delete.</param>
        /// <returns></returns>
        Task HardDeleteAsync(Guid id);

        /// <summary>
        /// Retrieves the unique identifier of a product by its slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        Task<Guid?> GetProductIdBySlugAsync(string slug, bool includeInactive = false);


        /// <summary>
        /// Retrieves a queryable collection of products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a queryable collection of products.</returns>
        public IQueryable<Product> GetQueryable(PaginationParams paginationParams);
    }
}

