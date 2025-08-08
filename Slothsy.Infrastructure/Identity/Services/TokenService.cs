using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Slothsy.Common.DTOs;
using Slothsy.Infrastructure.Data;
using Slothsy.Infrastructure.Identity.Config;
using Slothsy.Infrastructure.Identity.Entities;
using Slothsy.Infrastructure.Identity.Interfaces;
using Slothsy.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;

        public TokenService(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, AppDbContext dbContext)
        {
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<AuthenticationResponseDto> CreateToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Name, user.Email ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
             new Claim(JwtRegisteredClaimNames.Iat,
              DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
              ClaimValueTypes.Integer64),
            new Claim("FirstName", user.FirstName ?? ""),
            new Claim("LastName", user.LastName ?? "")
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTimeOffset expires = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expires.UtcDateTime,
            signingCredentials: creds
        );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };
            _dbContext.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();


            return new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = expires.UtcDateTime,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Email = user.Email ?? "",
                Roles = roles.ToList()
            };
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // We want to validate the token even if it's expired
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }
                else
                {
                    return null; // Invalid token type
                }

            }
            catch (Exception)
            {
                return null; // Token validation failed
            }
        }

        public async Task<AuthenticationResponseDto?> RefreshToken(RefreshTokenRequest request)
        {
            // 1.Check if the request contains valid tokens
            if (string.IsNullOrEmpty(request.AccessToken) || string.IsNullOrEmpty(request.RefreshToken))
            {
                return null;
            }

            // 2.Retrieving ClaimsPrincipal from the expired access token
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                return null;
            }

            // 3. retrieving user ID from the ClaimsPrincipal
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            // 4. Retrieving user from the database using UserManager
            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            // 5.Retrieving the existing refresh token from the user's refresh tokens if not revoked and not expired
            var refreshToken = user.RefreshTokens
                .FirstOrDefault(rt => rt.Token == request.RefreshToken && !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow);

            if (refreshToken == null)
            {
                return null;
            }

            // 6.revoking the existing refresh token
            refreshToken.IsRevoked = true;
            

            try
            {
                await _dbContext.SaveChangesAsync();

                // 7. Creating a new access token and refresh token
                var authResponse = await CreateToken(user);

               

                return authResponse;
            }
            catch (Exception ex)
            {
                //logging the exception (you can use a logging framework here)
                Console.WriteLine($"[RefreshToken] Exception: {ex.Message}");
                return null;
            }
        }
    }
    }

