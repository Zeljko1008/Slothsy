using Slothsy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterUserAsync(RegisterDto registerDto);
        Task<LoginResponseDto> LoginUserAsync(LoginDto loginDto);
    }
}
