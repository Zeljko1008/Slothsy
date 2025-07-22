using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Interfaces.RepositoryContracts
{
    public interface IProductColorVariantRepository
    {
        ///// <summary>
        ///// Retrieves a list of product color variants associated with the specified category slug.
        ///// </summary>
        ///// <param name="categorySlug">The unique slug identifier for the category. This parameter cannot be null or empty.</param>
        ///// <returns>A task that represents the asynchronous operation. The task result contains a list of  <see
        ///// cref="ProductColorVariant"/> objects associated with the specified category slug.  If no matching category
        ///// is found, the list will be empty.</returns>
        //Task<List<ProductColorVariant>> GetByCategorySlugAsync(string categorySlug);
        /// <summary>
        /// Retrieves a product color variant based on its unique slug identifier.
        /// </summary>
        /// <param name="slug">The unique slug identifier of the product color variant. Cannot be null or empty.</param>
        /// <returns>A <see cref="ProductColorVariant"/> object representing the product color variant if found;  otherwise, <see
        /// langword="null"/>.</returns>
        Task<ProductColorVariant?> GetBySlugAsync(string slug);


    /// <summary>
    /// Asynchronously adds a new product color variant to the system.
    /// </summary>
    /// <param name="variant">The product color variant to add. Cannot be <see langword="null"/>.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(ProductColorVariant variant);

        /// <summary>
        /// Asynchronously saves all changes made in the current context to the underlying database.
        /// </summary>
        /// <remarks>This method commits all tracked changes, including additions, modifications, and
        /// deletions, to the database. It ensures that the changes are persisted and returns the number of state
        /// entries written to the database.</remarks>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
        /// written to the database.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Retrieves a <see cref="ProductColorVariant"/> entity by its unique identifier, including related entities.
        /// </summary>
        /// <param name="id">The unique identifier of the <see cref="ProductColorVariant"/> to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see
        /// cref="ProductColorVariant"/>  with the specified identifier, including its related entities, or <see
        /// langword="null"/> if no matching entity is found.</returns>
        Task<ProductColorVariant?> GetByIdWithIncludesAsync(Guid id);

        /// <summary>
        /// Retrieves all product color variants associated with a specific product.    
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductColorVariant>> GetAllAsync(Guid productId);
    }
}
