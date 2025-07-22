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
    /// Configuration for the Product entity.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configures the schema for the Product entity.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Table name
            builder.ToTable("Products");

            // Primary key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(p => p.ShortDescription)
               .HasMaxLength(500);

            builder.Property(p => p.Description)
                .HasMaxLength(2000);

            builder.Property(p => p.Purpose)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.Fit)
            .HasConversion<int>()
            .IsRequired();

            builder.Property(p => p.Brand)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Gender)
                         .HasConversion<int>()
                         .IsRequired();

            builder.Property(p => p.AgeGroup)
              .HasConversion<int>()
              .IsRequired();

            builder.Property(p => p.Material)
             .HasConversion<int>()
             .IsRequired();

            builder.Property(p => p.Season)
                .HasConversion<int>()
                .IsRequired();


            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.SeoTitle)
                .HasMaxLength(100);

            builder.Property(p => p.SeoDescription)
                .HasMaxLength(500);

            builder.Property(p => p.Slug)
                .HasMaxLength(150);

            // Relationships
            builder
            .HasMany(pc => pc.ProductCategories)
            .WithOne(p => p.Product)
            .HasForeignKey(pc => pc.ProductId);

            builder.HasMany(p => p.ColorVariants)
               .WithOne(cv => cv.Product)
               .HasForeignKey(cv => cv.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
