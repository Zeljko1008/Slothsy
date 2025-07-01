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
    public class SizeOptionConfiguration : IEntityTypeConfiguration<SizeOption>
    {
        public void Configure(EntityTypeBuilder<SizeOption> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Label)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SizeType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(s => s.Order)
                .IsRequired();

            builder
             .HasMany(s => s.ProductVariants)
             .WithOne(pv => pv.SizeOption)
             .HasForeignKey(pv => pv.SizeOptionId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
