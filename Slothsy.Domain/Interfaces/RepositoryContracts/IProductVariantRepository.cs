using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Interfaces.RepositoryContracts
{
    public interface IProductVariantRepository
    {
        /// <summary>
        /// Retrieves a product variant by its unique slug.
        /// </summary>
        /// <param name="slug">The unique identifier for the product variant. This value cannot be <see langword="null"/> or empty.</param>
        /// <param name="includeInactive">A value indicating whether to include inactive product variants in the search.  <see langword="true"/> to
        /// include inactive variants; otherwise, <see langword="false"/>.</param>
        /// <returns>A <see cref="ProductVariant"/> object representing the product variant with the specified slug,  or <see
        /// langword="null"/> if no matching variant is found.</returns>
        Task<ProductVariant?> GetVariantBySlugAsync(string slug, bool includeInactive = false);

        
        /// <summary>
        /// Retrieves a product variant by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product variant to retrieve.</param>
        /// <param name="includeRelated">A value indicating whether related entities should be included in the result. <see langword="true"/> to
        /// include related entities; otherwise, <see langword="false"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the  <see
        /// cref="ProductVariant"/> if found; otherwise, <see langword="null"/>.</returns>
        Task<ProductVariant?> GetByIdAsync(Guid id);

        Task AddAsync(ProductVariant productVariant);

        Task SaveChangesAsync();

        Task<IEnumerable<ProductVariant>> GettAllAsync(Guid productColorVariantId);
    }

}
