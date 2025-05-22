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
    /// Configuration for the Order entity.
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// Configures the schema for the Order entity.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Table name
            builder.ToTable("Orders");

            // Primary key
            builder.HasKey(o => o.Id);

            // Required UserId (assuming we're tracking which user placed the order)
            builder.Property(o => o.UserId)
                .IsRequired();

            // Required OrderDate
            builder.Property(o => o.OrderDate)
                .IsRequired();

            // Required TotalAmount
            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Enum as string (optional - store OrderStatus as string instead of int)
            builder.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired();

            // One-to-many: Order → OrderItems
            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete order items if order is deleted

            // Optional: If you use a separate DeliveryMethod entity
            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting DeliveryMethod if used by orders
        }
    }
}
