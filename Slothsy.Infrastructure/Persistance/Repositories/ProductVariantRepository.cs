using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    /// Provides methods for accessing and managing product variants in the data store.
    /// </summary>
    /// <remarks>This repository is responsible for retrieving and interacting with product variant data, 
    /// including related entities such as products and images. It supports querying by slug  and filtering based on
    /// active or inactive product states.</remarks>
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ProductVariantRepository> _logger;

        public ProductVariantRepository(AppDbContext dbContext, ILogger<ProductVariantRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<inheritdoc />
        public async Task AddAsync(ProductVariant productVariant)
        {
          await _dbContext.ProductVariants.AddAsync(productVariant);

        }

        ///<inheritdoc />
        public async Task<ProductVariant?> GetByIdAsync(Guid id)
        {
            return await _dbContext.ProductVariants
                .Include(v => v.ProductColorVariant)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<ProductVariant>> GettAllAsync(Guid productColorVariantId)
        {
            return await _dbContext.ProductVariants
                .Where(v => v.ProductColorVariantId == productColorVariantId)
                .ToListAsync();
        }

        ///<inheritdoc />
        public async Task<ProductVariant?> GetVariantBySlugAsync(string slug, bool includeInactive = false)
        {
            return await _dbContext.ProductVariants
                 .Include(v => v.ProductColorVariant)
                 .FirstOrDefaultAsync();

        }

        ///<inheritdoc/>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
