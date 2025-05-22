using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration for the CartItem entity.
    /// </summary>
    public class CartItemConfiguration :IEntityTypeConfiguration<CartItem>
    {
        /// <summary>
        /// Configures the schema for the CartItem entity.
        /// </summary>
        /// <param name="builder">builder for configuring the entity</param>
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            // Table name
            builder.ToTable("CartItems");

            // Primary key
            builder.HasKey(ci => ci.Id);

            // Foreign key to Cart
            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.Items)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign key to Product
            builder.HasOne(ci => ci.Product)
                   .WithMany()
                   .HasForeignKey(ci => ci.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quantity is required and must be positive
            builder.Property(ci => ci.Quantity)
                   .IsRequired();
        }
    }
}
