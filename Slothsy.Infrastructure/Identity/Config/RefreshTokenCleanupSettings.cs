using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Config
{
    public class RefreshTokenCleanupSettings
    {
        public int CleanupIntervalHours { get; set; } = 24; // Default to 24 hours
    }
}
