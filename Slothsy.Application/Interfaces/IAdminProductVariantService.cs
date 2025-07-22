using Slothsy.Application.DTOs;
using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    public interface IAdminProductVariantService
    {
        /// <summary>
        /// Adds a new product variant to the system asynchronously.
        /// </summary>
        /// <remarks>This method performs validation on the provided <paramref name="dto"/> and ensures
        /// that the product variant  is associated with an existing product. The operation is performed
        /// asynchronously.</remarks>
        /// <param name="dto">The data transfer object containing the details of the product variant to be created.  This must include all
        /// required fields such as name, price, and associated product ID.</param>
        /// <returns>A <see cref="Guid"/> representing the unique identifier of the newly created product variant.</returns>
        Task<Guid> AddProductVariantAsync(CreateProductVariantDto dto);

        /// <summary>
        /// Retrieves a product variant by its unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch the product variant.  Ensure
        /// the <paramref name="id"/> is valid and corresponds to an existing product variant.</remarks>
        /// <param name="id">The unique identifier of the product variant to retrieve.</param>
        /// <returns>A <see cref="ProductVariantDto"/> representing the product variant if found;  otherwise, <see
        /// langword="null"/>.</returns>
        Task<ProductVariantDto?> GetProductVariantByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all product variants associated with a specific product color variant.    
        /// </summary>
        /// <param name="productColorVariantId"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductVariantDto>> GettAllAsync(Guid productColorVariantId);


    }
}
