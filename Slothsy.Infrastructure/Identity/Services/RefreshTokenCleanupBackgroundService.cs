using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Slothsy.Infrastructure.Identity.Config;
using Slothsy.Infrastructure.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Services
{
    public class RefreshTokenCleanupBackgroundService : BackgroundService
    {
        private readonly TimeSpan _cleanupInterval;
        private readonly ILogger<RefreshTokenCleanupBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public RefreshTokenCleanupBackgroundService( ILogger<RefreshTokenCleanupBackgroundService> logger, IServiceScopeFactory serviceScopeFactory, IOptions<RefreshTokenCleanupSettings> options)
        {
           
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _cleanupInterval = TimeSpan.FromHours(options?.Value?.CleanupIntervalHours ?? 24); 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RefreshTokenCleanupBackgroundService started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var cleanupService = scope.ServiceProvider.GetRequiredService<IRefreshTokenCleanupService>();
                    await cleanupService.CleanupExpiredRefreshTokensAsync();

                    _logger.LogInformation("Cleanup job completed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during refresh token cleanup.");
                }

                await Task.Delay(_cleanupInterval, stoppingToken);
            }

            _logger.LogInformation("RefreshTokenCleanupBackgroundService stopped.");
        }


    }
}
