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
    public class ColorOptionConfiguration : IEntityTypeConfiguration<ColorOption>
    {
        public void Configure(EntityTypeBuilder<ColorOption> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.HexCode)
                .HasMaxLength(10);

          

            builder.Property(c => c.Order)
                .IsRequired();

            

            // Relationship with ProductColorVariants
            builder.HasMany(c => c.ProductColorVariants)
                .WithOne(v => v.ColorOption)
                .HasForeignKey(v => v.ColorOptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
