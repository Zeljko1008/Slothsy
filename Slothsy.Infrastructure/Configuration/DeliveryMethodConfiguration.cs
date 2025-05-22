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
    /// Configuration for the DeliveryMethod entity.
    /// </summary>
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        /// <summary>
        /// Configures the schema for the DeliveryMethod entity.
        /// </summary>
        /// <param name="builder">builder for configuring the entity</param>
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            // Table name (optional, for clarity)
            builder.ToTable("DeliveryMethods");

            // Primary key
            builder.HasKey(dm => dm.Id);

            // Name is required and limited in length
            builder.Property(dm => dm.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Description is optional but limited in length
            builder.Property(dm => dm.Description)
                .HasMaxLength(500);

            // Delivery time is required and limited in length
            builder.Property(dm => dm.DeliveryTime)
                .IsRequired()
                .HasMaxLength(100);

            // Price is required and has precision
            builder.Property(dm => dm.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
