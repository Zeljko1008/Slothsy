using Microsoft.EntityFrameworkCore;
using Slothsy.Infrastructure.Data;
using Slothsy.Infrastructure.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Services
{
    public class RefreshTokenCleanupService : IRefreshTokenCleanupService
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenCleanupService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task CleanupExpiredRefreshTokensAsync()
        {
           var timeNow = DateTime.UtcNow;

            var expiredTokens= await _dbContext.RefreshTokens
                .Where(rt => rt.ExpiresAt <= timeNow || rt.IsRevoked)
                .ToListAsync();

            if (expiredTokens.Any())
                {
                _dbContext.RefreshTokens.RemoveRange(expiredTokens);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
