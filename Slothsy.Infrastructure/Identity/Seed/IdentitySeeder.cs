using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Slothsy.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Seed
{
    public class IdentitySeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public IdentitySeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            var roles = new[] { "Admin", "Manager", "User" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            await SeedUserAsync("Admin");
            await SeedUserAsync("Manager");
            await SeedUserAsync("User");
        }

        private async Task SeedUserAsync(string role)
        {
            var email = _configuration[$"SeedUserSettings:{role}:Email"];
            var password = _configuration[$"SeedUserSettings:{role}:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine($"[SEED] Skipping {role}, no email/password configured.");
                return;
            }

            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                Console.WriteLine($"[SEED] {role} user already exists.");
                return;
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = role,
                LastName = "Seeded"
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                Console.WriteLine($"[SEED] {role} user created successfully.");
            }
            else
            {
                Console.WriteLine($"[SEED ERROR] Failed to create {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}

