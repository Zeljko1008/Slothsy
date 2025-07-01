using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slothsy.Domain.Entities;
using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration for the Category entity.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Configures the schema for the Category entity.
        /// </summary>
        /// <param name="builder">Builder for configuring the entity.</param>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table name
            builder.ToTable("Categories");

            // Primary key
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Gender)
                 .HasConversion<int>()
                   .IsRequired(false);


            builder.Property(c => c.AgeGroup)
                     .HasConversion<int>()
                         .IsRequired(false);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(e => e.ShortDescription)
                .HasMaxLength(300);

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.Property(c => c.BannerImageUrl)
                .HasMaxLength(500);

            builder.Property(c => c.Order)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(e => e.SeoTitle)
                .HasMaxLength(100);

            builder.Property(e => e.SeoDescription)
                .HasMaxLength(500);

            builder.Property(e => e.Slug)
                .HasMaxLength(150);


            //relationship with Product
            builder.HasMany(pc => pc.ProductCategories)
                     .WithOne(c => c.Category)
                        .HasForeignKey(pc => pc.CategoryId)
                         .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Self-referencing relationship: one category can have many subcategories
            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.Subcategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


