using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkrinShop.Persistence.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();
        builder.Property(u => u.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.Email)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(u => u.Name)
            .IsUnique();
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
