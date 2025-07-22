using Microsoft.EntityFrameworkCore;
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
    public class SizeOptionRepository : ISizeOptionRepository
    {
        private readonly AppDbContext _dbContext;

        public SizeOptionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public Task AddAsync(SizeOption sizeOption)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SizeOption>> GetAllAsync()
        {
          return await _dbContext.SizeOptions
                .OrderBy(s => s.Label)
                .ToListAsync();
        }

        public async Task<SizeOption?> GetByIdAsync(Guid id)
        {
            return await _dbContext.SizeOptions
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SizeOption>> GetBySizeTypeAsync(SizeType sizeType)
        {
            return await _dbContext.SizeOptions
                .Where(s => s.SizeType == sizeType)
                .OrderBy(s => s.Label)
                .ToListAsync();
        }

        public Task UpdateAsync(SizeOption sizeOption)
        {
            throw new NotImplementedException();
        }
    }
}
