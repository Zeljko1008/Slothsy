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
    /// Configuration for the Cart entity.
    /// </summary>
    public class CartConfiguration:IEntityTypeConfiguration<Cart>
    {
        /// <summary>
        /// Configures the schema for the Cart entity.
        /// </summary>
        /// <param name="builder">builder for configuring the entity</param>
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            // Table name
            builder.ToTable("Carts");

            // Primary key
            builder.HasKey(c => c.Id);

            // UserId can be nullable (for anonymous users)
            builder.Property(c => c.UserId)
                   .HasMaxLength(450) // Typical max length for ASP.NET Identity user IDs
                   .IsRequired(false);

            // LastModified is required
            builder.Property(c => c.LastModified)
                   .IsRequired();

            // One-to-many relationship: Cart has many CartItems
            builder.HasMany(c => c.Items)
                   .WithOne(i => i.Cart)
                   .HasForeignKey(i => i.CartId)
                   .OnDelete(DeleteBehavior.Cascade); // When Cart is deleted, delete related Items
        }
    }
}
