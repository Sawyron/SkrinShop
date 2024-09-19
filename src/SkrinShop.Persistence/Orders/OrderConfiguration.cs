using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkrinShop.Persistence.Orders;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();
        builder.Property(o => o.Date)
            .HasColumnType("timestamp without time zone")
            .IsRequired();
        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .IsRequired();
    }
}
