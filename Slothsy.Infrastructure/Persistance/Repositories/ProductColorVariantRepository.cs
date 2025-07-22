using Microsoft.EntityFrameworkCore;
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

    public class ProductColorVariantRepository : IProductColorVariantRepository
    {
        private readonly AppDbContext _context;

        public ProductColorVariantRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        ///<inheritdoc />
        public async Task AddAsync(ProductColorVariant variant)
        {
           await _context.ProductColorVariants.AddAsync(variant);
        }

        public async Task<IEnumerable<ProductColorVariant>> GetAllAsync(Guid productId)
        {
          return  await _context.ProductColorVariants
                .Include(cv => cv.ColorOption)
                .Include(cv => cv.Images)
                //.Include(cv => cv.Variants)
                //.ThenInclude(v => v.SizeOption)
                .Where(cv => cv.ProductId == productId)
                .ToListAsync();

            
        }

        /////<inheritdoc />

        //public Task<List<ProductColorVariant>> GetByCategorySlugAsync(string categorySlug)
        //{
        //    throw new NotImplementedException();
        //}

        ///<inheritdoc />

        public async Task<ProductColorVariant?> GetByIdWithIncludesAsync(Guid id)
        {
          return await _context.ProductColorVariants
            .Include(cv => cv.ColorOption)
            .Include(cv => cv.Images)
            .Include(cv => cv.Variants)
            .Include(cv=> cv.Product)
            .FirstOrDefaultAsync(cv => cv.Id == id);
        }

        ///<inheritdoc />

        public async Task<ProductColorVariant?> GetBySlugAsync(string slug)
        {
            return await _context.ProductColorVariants
            .Include(cv => cv.Product)
            .Include(cv => cv.ColorOption)
            .Include(cv => cv.Images)
            .Include(cv => cv.Variants)
                .ThenInclude(v => v.SizeOption)
            .FirstOrDefaultAsync(cv => cv.Slug == slug);
        }

        ///<inheritdoc />

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();


        }
    }
}
