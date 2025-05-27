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
        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting product with ID:{Id}", id);
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Product with ID:{Id} deleted successfully.", id);

            }
            _logger.LogWarning("Product with ID:{Id} not found.", id);

        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all products.");
            return await _dbContext.Products.ToListAsync();

        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId)
        {
           _logger.LogInformation("Retrieving products for category ID:{CategoryId}", categoryId);
            return await _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
           
        }
        /// <inheritdoc/>
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", id);
            return await _dbContext.Products.FindAsync(id);

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

        public IQueryable<Product> GetQueryable()
        {
            return _dbContext.Products.AsNoTracking().OrderBy(p => p.Name); 
        }
    }
}
