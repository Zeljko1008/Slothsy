using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Common.DTOs
{
    public class AuthenticationResponseDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();
    }
}
