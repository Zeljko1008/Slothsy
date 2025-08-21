using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slothsy.Application.DTOs;
using Slothsy.Infrastructure.Identity.Entities;
using Slothsy.Infrastructure.Identity.Interfaces;
using Slothsy.Infrastructure.Identity.Models;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IAuthService authService, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterUserAsync(registerDto);

            if (!result.Success)
                return BadRequest(new { title = "Registration Failed", message = result.Message });

            return Ok(new { title = "Registration Successful", message = result.Message, userId = result.UserId });
        }
       

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginUserAsync(loginDto);

            if (!result.Success)
                return Unauthorized(new { title = "Login Failed", message = result.Message });

            return Ok(result);
        }


        [HttpPost("confirm-email-dev")]
        public async Task<IActionResult> ConfirmEmailDev([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found.");

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Email confirmation failed.");

            return Ok("Email confirmed successfully (dev only).");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Refresh token is required.");

            var response = await _tokenService.RefreshToken(request);
            if (response == null)
                return Unauthorized("Invalid or expired refresh token.");

            return Ok(response);
        }
    }
}
