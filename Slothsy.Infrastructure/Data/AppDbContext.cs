using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Slothsy.Domain.Entities;
using Slothsy.Infrastructure.Identity.Entities;
using Slothsy.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Data
{
    /// <summary>
    /// Database context for the application.
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();


        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<DeliveryMethod> DeliveryMethods => Set<DeliveryMethod>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<ProductColorVariant> ProductColorVariants => Set<ProductColorVariant>();

        public DbSet<ProductVariantImage> ProductVariantImages => Set<ProductVariantImage>();
        public DbSet<SizeOption> SizeOptions => Set<SizeOption>();
        public DbSet<ColorOption> ColorOptions => Set<ColorOption>();





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configuration classes automatically
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
