using Slothsy.Application.DTOs;
using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    public interface IProductVariantService
    {
        /// <summary>
        /// Retrieves a product variant based on the specified slug.
        /// </summary>
        /// <param name="slug">The unique identifier for the product variant, typically used in URLs.  This value cannot be null or empty.</param>
        /// <returns>A <see cref="ProductVariant"/> object if a matching product variant is found;  otherwise, <see
        /// langword="null"/>.</returns>
        Task<ProductVariantDto?> GetBySlugAsync(string slug);

    }
}
