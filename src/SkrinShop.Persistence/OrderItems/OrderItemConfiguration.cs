using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkrinShop.Persistence.OrderItems;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();
        builder.Property(i => i.Quantity)
            .IsRequired();
        builder.Property(i => i.Price)
            .IsRequired();
        builder.HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .IsRequired();
        builder.HasOne(i => i.Order)
            .WithMany()
            .HasForeignKey(i => i.OrderId)
            .IsRequired();
    }
}
