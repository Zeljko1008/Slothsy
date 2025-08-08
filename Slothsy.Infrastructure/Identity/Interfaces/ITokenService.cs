using Slothsy.Common.DTOs;
using Slothsy.Infrastructure.Identity.Entities;
using Slothsy.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticationResponseDto> CreateToken(ApplicationUser user);

        Task<AuthenticationResponseDto?> RefreshToken(RefreshTokenRequest request);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
