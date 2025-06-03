using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Slothsy.Common.Pagination;
using Slothsy.Domain.Entities;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using Slothsy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repository for data access operations related to products.  
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly AppDbContext _dbContext;

        public ProductRepository(ILogger<ProductRepository> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Product> AddAsync(Product product)
        {
            _logger.LogInformation("Adding a new product with name:{Name}", product.Name);
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Product added successfully.");
            return product;



        }

        /// <inheritdoc/>
        public async Task SoftDeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting product with ID:{Id}", id);
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                product.IsActive = false; // Soft delete
                await _dbContext.SaveChangesAsync();


            }
            _logger.LogWarning("Product with ID:{Id} not found.", id);

        }
        /// <inheritdoc/>
        public async Task<PagedResult<Product>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _dbContext.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!paginationParams.IncludeInactive)
            {
                query = query.Where(p => p.IsActive);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Name)
                .Skip(paginationParams.Skip)
                .Take(paginationParams.ValidatedPageSize)
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} products out of {TotalCount} total.", items.Count, totalCount);

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.ValidatedPageSize
            };


        }
        /// <inheritdoc/>
        public async Task<PagedResult<Product>> GetByCategoryIdAsync(Guid categoryId, PaginationParams paginationParams)
        {
            _logger.LogInformation("Retrieving products for category ID:{CategoryId}", categoryId);
            var query = _dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId);

            if (!paginationParams.IncludeInactive)
            {
                query = query.Where(p => p.IsActive);
            }
            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Name)
                .Skip(paginationParams.Skip)
                .Take(paginationParams.ValidatedPageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.ValidatedPageSize
            };


        }
        /// <inheritdoc/>
        public async Task<Product?> GetByIdAsync(Guid id, bool includeInactive)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", id);
            return await _dbContext.Products
                 .Include(p => p.Category)
                 .Where(p => p.Id == id && (includeInactive || p.IsActive))
                 .FirstOrDefaultAsync();

        }
        /// <inheritdoc/>
        public async Task<List<Product>> SearchByNameAsync(string name)
        {
            _logger.LogInformation("Searching products by name: {Name}", name);
            var lowered = name.ToLower();

            return await _dbContext.Products
                .Include(p => p.Category)
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{lowered}%"))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Product product)
        {
            _logger.LogInformation("Updating product with ID:{Id}", product.Id);
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Product with ID:{Id} updated successfully.", product.Id);

        }

        /// <inheritdoc/>
        public IQueryable<Product> GetQueryable(PaginationParams paginationParams)
        {
            return _dbContext.Products
        .AsNoTracking()
        .Include(p => p.Category)
        .Where(p => paginationParams.IncludeInactive || p.IsActive)
        .OrderBy(p => p.Name);
        }

        /// <inheritdoc/>
        public async Task HardDeleteAsync(Guid id)
        {
            _logger.LogInformation("Hard deleting product with ID:{Id}", id);
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Product with ID:{Id} hard deleted successfully.", id);
            }
            else
            {
                _logger.LogWarning("Product with ID:{Id} not found for hard delete.", id);
            }
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetProductIdBySlugAsync(string slug, bool includeInactive = false)
        {
            _logger.LogInformation("Retrieving product ID by slug: {Slug}", slug);

            return await _dbContext.Products
                .Where(p => p.Slug == slug && (includeInactive || p.IsActive))
                .Select(p => (Guid?)p.Id)
                .FirstOrDefaultAsync();

        }
    }
}
