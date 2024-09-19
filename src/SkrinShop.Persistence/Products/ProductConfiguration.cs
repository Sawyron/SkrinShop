using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkrinShop.Persistence.Products;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}
