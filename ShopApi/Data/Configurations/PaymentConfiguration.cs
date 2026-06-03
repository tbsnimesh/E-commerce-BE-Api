using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;
using ShopApi.Domain.Enums;

namespace ShopApi.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
               .IsRequired()
               .HasPrecision(18, 2);

        builder.Property(p => p.Method)
               .IsRequired()
               .HasMaxLength(100);

        // Store enum as string — same reasoning as OrderStatus
        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50)
               .HasDefaultValue(PaymentStatus.Pending);

        // Nullable — only set once the payment gateway confirms the transaction
        builder.Property(p => p.TransactionId)
               .HasMaxLength(200);

        builder.Property(p => p.PaidAt);

        // One-to-one with Order — relationship owner is Payment (it holds the FK)
        // Configured here; OrderConfiguration also references it via HasOne(o => o.Payment)
        builder.HasOne(p => p.Order)
               .WithOne(o => o.Payment)
               .HasForeignKey<Payment>(p => p.OrderId)
               .OnDelete(DeleteBehavior.Restrict);

        // No soft delete — financial record; must be immutable
    }
}
