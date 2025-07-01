using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Slothsy.Domain.Entities;
using Slothsy.Domain.Enums;
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
    /// Repository for data access operations related to product categories.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _dbContext;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<Category> AddAsync(Category category)
        {
            _logger.LogInformation("Adding a new category with name: {Name}", category.Name);
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Category added successfully with ID: {Id}", category.Id);
            return category;

        }
        /// <inheritdoc/>
        public async Task SoftDeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting category with ID: {Id}", id);
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                category.IsActive = false; // Soft delete
                await _dbContext.SaveChangesAsync();

            }
            else
            {
                _logger.LogWarning("Category with ID: {Id} not found.", id);
            }
        }
        /// <inheritdoc/>
        public async Task<List<Category>> GetAllCategoriesAsync(bool includeInactive = false)
        {
            _logger.LogInformation("Retrieving all categories.");
            return await _dbContext.Categories
                .Where(c => includeInactive || c.IsActive)
                .OrderBy(c => c.Order)
                .ToListAsync();


        }
        /// <inheritdoc/>
        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching category with ID: {CategoryId}", id);
            return await _dbContext.Categories
                .FindAsync(id);


        }
        /// <inheritdoc/>
        public async Task<List<Category>> GetMainCategoriesAsync(
     bool includeInactive = false,
     Gender? gender = null,
     AgeGroup? ageGroup = null)
        {
            _logger.LogInformation("Retrieving main categories with filters: includeInactive={IncludeInactive}, gender={Gender}, ageGroup={AgeGroup}",
                includeInactive, gender, ageGroup);

            var query = _dbContext.Categories
                .Where(c => c.ParentCategoryId == null);

            if (!includeInactive)
                query = query.Where(c => c.IsActive);

            if (gender.HasValue)
                query = query.Where(c => c.Gender == gender.Value);

            if (ageGroup.HasValue)
                query = query.Where(c => c.AgeGroup == ageGroup.Value);

            return await query.OrderBy(c => c.Order).ToListAsync();
        }
        /// <inheritdoc/>
        public async Task<List<Category>> GetSubcategoriesAsync(Guid parentCategoryId, bool includeInactive = false)
        {
            _logger.LogInformation("Retrieving subcategories for parent category ID: {ParentCategoryId}", parentCategoryId);
            return await _dbContext.Categories
                .Where(c => c.ParentCategoryId == parentCategoryId && (includeInactive ||c.IsActive))
                .OrderBy(c => c.Order)
                .ToListAsync();
        }
        ///<inheritdoc/>
        public async Task HardDeleteAsync(Guid id)
        {
           _logger.LogInformation("Hard deleting category with ID: {Id}", id);
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Category with ID: {Id} not found for hard delete.", id);
                
            }
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(Category category)
        {
            _logger.LogInformation("Updating category with ID: {Id}", category.Id);

            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Category with ID: {Id} updated successfully.", category.Id);
        }

        ///<inheritdoc/>
        public async Task<Guid?> GetCategoryIdBySlugAsync(string slug)
        {
            return await _dbContext.Categories
       .Where(c => c.Slug == slug && c.IsActive)
       .Select(c => (Guid?)c.Id)
       .FirstOrDefaultAsync();
        }

        ///<inheritdoc/>
        public async Task<Category?> GetBySlugAsync(string slug, bool includeInactive = false)
        {
            var query = _dbContext.Categories.AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(c => c.IsActive);
            }

            return await query.FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<List<Guid>> GetCategoryAndSubcategoryIdsBySlugAsync(string slug)
        {
            var rootCategory = await _dbContext.Categories
        .FirstOrDefaultAsync(c => c.Slug == slug);

            if (rootCategory == null)
                return new List<Guid>();

            var allCategoryIds = new List<Guid> { rootCategory.Id };

            // recursive function to get all subcategories
            async Task GetSubcategories(Guid parentId)
            {
                var children = await _dbContext.Categories
                    .Where(c => c.ParentCategoryId == parentId)
                    .ToListAsync();

                foreach (var child in children)
                {
                    allCategoryIds.Add(child.Id);
                    await GetSubcategories(child.Id);
                }
            }

            await GetSubcategories(rootCategory.Id);

            return allCategoryIds;
        }
    }
}
