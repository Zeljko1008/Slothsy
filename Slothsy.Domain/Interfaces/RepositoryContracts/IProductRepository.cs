using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product with the specified ID, or null if not found.</returns>
        Task<Product?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all products that belong to a specific category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <returns>A list of products in the specified category.</returns>
        Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);
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
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Retrieves a queryable collection of products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a queryable collection of products.</returns>
     public IQueryable<Product> GetQueryable();
    }
}

