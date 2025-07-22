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
    public class ColorOptionRepository : IColorOptionRepository
    {
        private readonly AppDbContext _dbContext;

        public ColorOptionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ColorOption> GetByIdAsync(Guid id)
        {
           
            var colorOption = await _dbContext.ColorOptions.FindAsync(id);
            if (colorOption == null)
            {
                throw new KeyNotFoundException($"ColorOption with ID {id} was not found.");
            }
            return colorOption;
        }
    }
}
