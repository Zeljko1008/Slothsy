
using Microsoft.AspNetCore.Identity;
using Slothsy.Application.DTOs;
using Slothsy.Infrastructure.Identity.Entities;
using Slothsy.Infrastructure.Identity.Interfaces;

namespace Slothsy.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ITokenService _tokenService;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        public async Task<LoginResponseDto> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    Title = "Login Failed",
                    Message = "Invalid credentials."
                };
            }

            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!passwordCheck.Succeeded)
            {
                return new LoginResponseDto
                {
                    Title = "Login Failed",
                    Message = "Invalid credentials."
                };
            }

            // Dohvati korisnikove uloge
            var roles = await _userManager.GetRolesAsync(user);
            Console.WriteLine(string.Join(",", roles));

            // Generiraj Access Token
            var authenticationResponse = await _tokenService.CreateToken(user);

          

            return new LoginResponseDto
            {
                Success = true,
                Title = "Login Successful",
                Message = "User logged in successfully.",
                UserId = user.Id,
                FirstName = user.FirstName,
                AccessToken = authenticationResponse.AccessToken,
                RefreshToken = authenticationResponse.RefreshToken,
                AccessTokenExpiration = authenticationResponse.ExpiresAt,
                Roles = roles.ToList()
            };
        }

        public async Task<RegisterResponseDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new RegisterResponseDto
                {
                    Success = false,
                    Title = "Registration Failed",
                    Message = "Email is already in use."
                };
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
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResponseDto
                {
                    Success = false,
                    Title = "Registration Failed",
                    Message = errors
                };
            }

            // Dodaj ulogu "User" (ako ne postoji, kreiraj)
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            await _userManager.AddToRoleAsync(user, "User");

            return new RegisterResponseDto
            {
                Success = true,
                Message = "User registered successfully.",
                UserId = user.Id
            };
        }
    }
}
