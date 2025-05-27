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

namespace Slothsy.Application.Services
{
    /// <summary>
    /// Provides read-only operations for retrieving product data.
    /// </summary>
    public class ProductReadService : IProductReadService
    {
        private readonly IProductRepository _productRepository;
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
        public ProductReadService(IProductRepository productRepository, IMapper mapper, ILogger<ProductReadService> logger)
        {
            _productRepository = productRepository
                                 ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(productRepository)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all products");
            // 1. Repository call - async, no business logic here.
            //    The repository should internally Include(p => p.Category)
            //    so the mapping to CategoryName works.
            var  products =await _productRepository.GetAllAsync();

            _logger.LogInformation("Retrieved {Count} products", products.Count());

            // 2. Map to DTOs.  AutoMapper handles the heavy lifting.
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
        /// <inheritdoc />
        public async Task<PagedResult<ProductDto>> GetByCategoryIdAsync(Guid categoryId, PaginationParams paginationParams)
        {
            _logger.LogInformation("Fetching products by CategoryId: {CategoryId}", categoryId);
            var query =  _productRepository.GetQueryable();
         query = query.Where(p => p.CategoryId == categoryId);

            var pagedResult = await query.ToPagedResultAsync(paginationParams);

            var dtoItems = _mapper.Map<IEnumerable<ProductDto>>(pagedResult.Items).ToList();

            return new PagedResult<ProductDto>
            {
                Items = dtoItems,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };


        }
        /// <inheritdoc />
        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting product by ID {Id}", id);

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {Id} not found", id);
                throw new NotFoundException($"Product with ID '{id}' was not found.");
            }
        
            _logger.LogInformation("Retrieved product with ID {Id}", id);
            return _mapper.Map<ProductDto>(product);

        }
        /// <inheritdoc />
        public async Task<PagedResult<ProductDto>> SearchByNameAsync(string name, PaginationParams paginationParams)
        {
            _logger.LogInformation("Searching products by name: {Name}", name);

            var query =  _productRepository.GetQueryable();
                query = query.Where(p => p.Name.Contains(name));

            var pagedResult = await query.ToPagedResultAsync(paginationParams);

            var dtoItems = _mapper.Map<IEnumerable<ProductDto>>(pagedResult.Items).ToList();

            return new PagedResult<ProductDto>
            {
                Items = dtoItems,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }
        ///<inheritdoc/>
        public async Task<PagedResult<ProductDto>> GetPagedAsync(PaginationParams paginationParams)
        {
            var query =  _productRepository.GetQueryable(); 

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip(paginationParams.Skip)
                .Take(paginationParams.ValidatedPageSize)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<ProductDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize
            };
        }
    }
}

