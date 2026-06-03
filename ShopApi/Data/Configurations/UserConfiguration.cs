using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;

namespace ShopApi.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // 1. Table name
        builder.ToTable("Users");

        // 2. Primary key
        builder.HasKey(u => u.Id);

        // 3. FirstName: required, max 100
        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(50);

        // 4. LastName: required, max 100
        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(50);

        // 5. Email: required, max 256, unique
        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(256);

        builder.HasIndex(u => u.Email)
               .IsUnique();

        // 6. PasswordHash: required, max 512
        builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(500);

        // 7. Role: required, max 50, default "Customer"
        builder.Property(u => u.Role)
               .IsRequired()
               .HasMaxLength(20)
               .HasDefaultValue("Customer");

        // 8. CreatedAt: stored as UTC
        builder.Property(u => u.CreatedAt)
             .HasDefaultValueSql("GETUTCDATE()");

        // 9. Active / deactivated. Not soft-delete:
        //    A user record is preserved permanently (legal/audit reasons).
        //    Deactivating an account just blocks login — orders, history, carts stay queryable.
        builder.Property(u => u.IsActive)
               .IsRequired()
               .HasDefaultValue(true);
    }
}
