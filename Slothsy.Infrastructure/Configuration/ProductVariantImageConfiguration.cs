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
    public class ProductVariantImageConfiguration : IEntityTypeConfiguration<ProductVariantImage>
    {
        public void Configure(EntityTypeBuilder<ProductVariantImage> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(i => i.Order)
                .IsRequired();

            builder.Property(i => i.IsMain)
                .IsRequired();

            builder.HasOne(i => i.ProductColorVariant)
            .WithMany(pc => pc.Images)
            .HasForeignKey(i => i.ProductColorVariantId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
