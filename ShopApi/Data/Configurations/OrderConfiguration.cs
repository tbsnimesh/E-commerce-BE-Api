using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;
using ShopApi.Domain.Enums;

namespace ShopApi.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderDate)
               .IsRequired();

        // Store enum as its string name ("Pending", "Paid" …) so the DB is human-readable.
        // Storing as int is the EF default, but ints break silently if you ever reorder enum values.
        builder.Property(o => o.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50)
               .HasDefaultValue(OrderStatus.Pending);

        builder.Property(o => o.TotalAmount)
               .IsRequired()
               .HasPrecision(18, 2);

        // Shipping address columns — flattened onto the Orders table (no separate ShippingAddress entity)
        builder.Property(o => o.ShippingStreet).IsRequired().HasMaxLength(200);
        builder.Property(o => o.ShippingCity).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ShippingState).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ShippingPostalCode).IsRequired().HasMaxLength(20);
        builder.Property(o => o.ShippingCountry).IsRequired().HasMaxLength(100);

        // Order belongs to one User
        builder.HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        // Order has many OrderItems
        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

    
        // No soft delete — orders are legal records and must never be destroyed
    }
}
