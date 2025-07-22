using Slothsy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    public interface IAdminProductColorVariantService
    {
        /// <summary>
        /// Asynchronously adds a new color variant to the specified product.
        /// </summary>
        /// <remarks>Ensure that the product specified by <paramref name="productId"/> exists before
        /// calling this method. The <paramref name="dto"/> parameter must contain valid data for the color variant to
        /// be successfully created.</remarks>
        /// <param name="productId">The unique identifier of the product to which the color variant will be added.</param>
        /// <param name="dto">An object containing the details of the color variant to be created.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unique identifier of the
        /// newly created color variant.</returns>
         Task<Guid> AddColorVariantAsync(CreateProductColorVariantDto dto);

        /// <summary>
        /// Retrieves a product color variant by its unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch the color variant data.
        /// Ensure the <paramref name="colorVariantId"/> is valid and corresponds to an existing color
        /// variant.</remarks>
        /// <param name="colorVariantId">The unique identifier of the color variant to retrieve.</param>
        /// <returns>A <see cref="ProductColorVariantDto"/> representing the color variant if found; otherwise, <see
        /// langword="null"/>.</returns>
        Task<ProductColorVariantDto?> GetColorVariantByIdAsync(Guid colorVariantId);

        /// <summary>
        /// Retrives IEnumerable of all color variants for a specific product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductColorVariantDto?>> GetAllAsync(Guid productId);

    }
}
