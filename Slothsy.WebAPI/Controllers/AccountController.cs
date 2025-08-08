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
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email,
                Email = registerDto.Email

            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Problem(errors);
            }

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Console.WriteLine($"[REGISTER] Confirmation token for {user.Email}:");
            Console.WriteLine(confirmationToken);
            //TODO: Implement email confirmation logic

            return Ok("User registered successfully. Please check your email for confirmation.");

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            if (!user.EmailConfirmed)
            {
                return Unauthorized("Email not confirmed. Please check your email for confirmation.");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var response = await _tokenService.CreateToken(user);
            return Ok(response);

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
            if (string.IsNullOrEmpty(request.AccessToken) || string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest("Access token and refresh token are required.");
            }
            var response = await _tokenService.RefreshToken(request);
            if (response == null)
            {
                return Unauthorized("Invalid refresh token.");
            }
            return Ok(response);


        }
    }
}
