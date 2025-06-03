using AutoMapper;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slothsy.Domain.Entities;
using Slothsy.Application.Exceptions;
using Slothsy.Application.Models;
using Slothsy.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Slothsy.Common.Pagination;

namespace Slothsy.Application.Services
{
    /// <summary>
    /// Provides read-only operations for retrieving product data.
    /// </summary>
    public class ProductReadService : IProductReadService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates an instance of <see cref="ProductReadService"/>.
        /// </summary>
        /// <param name="productRepository">
        /// Repository responsible for data access.  
        /// Must return products *with* <c>Category</c> loaded so that
        /// <c>CategoryName</c> maps correctly.
        /// </param>
        /// <param name="mapper">AutoMapper instance for entity-DTO projection.</param>
        /// <param name="logger">Logger instance for logging operations.</param>
        public ProductReadService(IProductRepository productRepository, IMapper mapper, ILogger<ProductReadService> logger, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository
                                 ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository
                                  ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(productRepository)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <inheritdoc />
        public async Task<PagedResult<ProductDto>> GetAllAsync(PaginationParams paginationParams)
        {
            _logger.LogInformation("Getting paged products: Page {Page}, Size {Size}, IncludeInactive: {IncludeInactive}",
        paginationParams.PageNumber, paginationParams.PageSize, paginationParams.IncludeInactive);

            var  paginatedResult =await _productRepository.GetAllAsync(paginationParams);

            var dtoItems = _mapper.Map<List<ProductDto>>(paginatedResult.Items);

            return new PagedResult<ProductDto>
            {
                Items = dtoItems,
                TotalCount = paginatedResult.TotalCount,
                PageNumber = paginatedResult.PageNumber,
                PageSize = paginatedResult.PageSize
            };
        }
        /// <inheritdoc />
        public async Task<PagedResult<ProductDto>> GetByCategorySlugAsync(string slug, PaginationParams paginationParams)
        {
           
            var categoryId = await _categoryRepository.GetCategoryIdBySlugAsync(slug);

            if (categoryId == null)
            {
                _logger.LogWarning("Category with slug '{Slug}' not found", slug);
                throw new NotFoundException($"Category with slug '{slug}' was not found.");
            }

            var result = await _productRepository.GetByCategoryIdAsync(categoryId.Value, paginationParams);

            var itemsDto = _mapper.Map<List<ProductDto>>(result.Items);

            return new PagedResult<ProductDto>
            {
                Items = itemsDto,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };


        }
        /// <inheritdoc />
        public async Task<ProductDto?> GetProductBySlugAsync(string slug, bool includeInactive)
        {
           

            var categoryId = await _productRepository.GetProductIdBySlugAsync(slug);


            if (categoryId == null)
            {
                _logger.LogWarning("Product with slug '{Slug}' not found", slug);
                throw new NotFoundException($"Product with slug '{slug}' was not found.");
            }

            var result = await _productRepository.GetByIdAsync(categoryId.Value, includeInactive);


            return _mapper.Map<ProductDto>(result);

        }
        /// <inheritdoc />
        public async Task<PagedResult<ProductDto>> SearchByNameAsync(string name, PaginationParams paginationParams)
        {
            _logger.LogInformation("Searching products by name: {Name}", name);

            var query = _productRepository.GetQueryable(paginationParams)
                                   .Where(p => p.Name.Contains(name));


            var pagedResult = await query.ToPagedResultAsync(paginationParams);

            var dtoItems = _mapper.Map<List<ProductDto>>(pagedResult.Items);

            return new PagedResult<ProductDto>
            {
                Items = dtoItems,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }
        ///<inheritdoc/>
        //public async Task<PagedResult<ProductDto>> GetPagedAsync(PaginationParams paginationParams)
        //{
        //    var query =  _productRepository.GetQueryable(); 

        //    var totalCount = await query.CountAsync();
        //    var items = await query
        //        .Skip(paginationParams.Skip)
        //        .Take(paginationParams.ValidatedPageSize)
        //        .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
        //        .ToListAsync();

        //    return new PagedResult<ProductDto>
        //    {
        //        Items = items,
        //        TotalCount = totalCount,
        //        PageNumber = paginationParams.PageNumber,
        //        PageSize = paginationParams.PageSize
        //    };
        //}
    }
}

