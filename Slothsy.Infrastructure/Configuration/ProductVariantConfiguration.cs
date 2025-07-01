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

            builder.Property(v => v.ProductId)
                .IsRequired();

            builder.Property(v => v.SizeOptionId)
                .IsRequired();

            builder.Property(v => v.ColorOptionId)
                .IsRequired();

            builder.Property(v => v.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(v => v.DiscountPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(v => v.StockQuantity)
                .IsRequired();


            // Relationships
            builder.HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.SizeOption)
                .WithMany()
                .HasForeignKey(v => v.SizeOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.ColorOption)
                .WithMany(c => c.ProductVariants)
                .HasForeignKey(v => v.ColorOptionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
