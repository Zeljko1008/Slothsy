using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Configuration
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Slug)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(v => v.ProductColorVariantSlug)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(v => v.SizeLabel)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(v => v.StockQuantity)
                .IsRequired();

            builder.HasOne(v => v.ProductColorVariant)
                .WithMany(pc => pc.Variants)
                .HasForeignKey(v => v.ProductColorVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.SizeOption)
                .WithMany()
                .HasForeignKey(v => v.SizeOptionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
