using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
            builder.Property(e => e.PictureURL).IsRequired();
            builder.HasOne(e => e.ProductBrand).WithMany()
                   .HasForeignKey(e => e.ProductBrandId);

            builder.HasOne(e => e.ProductType).WithMany()
                   .HasForeignKey(e => e.ProductTypeId);
        }
    }
}
