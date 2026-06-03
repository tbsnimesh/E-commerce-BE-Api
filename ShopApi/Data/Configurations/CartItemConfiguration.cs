using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;

namespace ShopApi.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Quantity)
               .IsRequired();

        // A product can appear only once per cart — enforce at the DB level
        builder.HasIndex(ci => new { ci.CartId, ci.ProductId })
               .IsUnique();

        // CartItem belongs to one Cart
        builder.HasOne(ci => ci.Cart)
               .WithMany(c => c.CartItems)
               .HasForeignKey(ci => ci.CartId)
               .OnDelete(DeleteBehavior.Cascade);

        // CartItem references one Product
        // Restrict: deleting a soft-deleted product must not wipe cart items
        builder.HasOne(ci => ci.Product)
               .WithMany(p => p.CartItems)
               .HasForeignKey(ci => ci.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        // Hide cart items whose product has been soft-deleted.
        // No User check needed: User is not soft-deletable in this system.
        builder.HasQueryFilter(ci => !ci.Product.IsDeleted);
    }
}
