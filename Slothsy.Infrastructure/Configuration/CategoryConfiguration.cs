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
                .HasMaxLength(70);

            builder.Property(e => e.SeoDescription)
                .HasMaxLength(160);

            builder.Property(e => e.Slug)
                .HasMaxLength(100);


            // One-to-many relationship with Product
            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Self-referencing relationship: one category can have many subcategories
            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.Subcategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}


