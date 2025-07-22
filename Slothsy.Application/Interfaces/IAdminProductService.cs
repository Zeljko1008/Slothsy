using Slothsy.Application.DTOs;
using Slothsy.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    public interface IAdminProductService
    {
        Task<PagedResult<ProductDto>> GetAllAsync(PaginationParams paginationParams);
        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        Task<ProductDto?> GetProductById(Guid id, bool includeInactive = false);

        /// <summary>
        /// Asynchronously adds a new product to the system and returns the unique identifier of the created product.
        /// </summary>
        Task<Guid> AddProductAsync(CreateProductDto createProductDto);
    }


}
