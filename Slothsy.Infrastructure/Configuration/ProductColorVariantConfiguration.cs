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
    public class ProductColorVariantConfiguration : IEntityTypeConfiguration<ProductColorVariant>
    {
        public void Configure(EntityTypeBuilder<ProductColorVariant> builder)
        {
            builder.HasKey(cv => cv.Id);

            builder.Property(cv => cv.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pc => pc.Price)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.Property(pc => pc.DiscountPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.HasOne(cv => cv.Product)
                .WithMany(p => p.ColorVariants)
                .HasForeignKey(cv => cv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cv => cv.ColorOption)
               .WithMany(co => co.ProductColorVariants)
               .HasForeignKey(cv => cv.ColorOptionId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(cv => cv.Variants)
                .WithOne(v => v.ProductColorVariant)
                .HasForeignKey(v => v.ProductColorVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cv => cv.Images)
                .WithOne(i => i.ProductColorVariant)
                .HasForeignKey(i => i.ProductColorVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
