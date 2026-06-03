using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;

namespace ShopApi.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Street)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(a => a.City)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.State)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.PostalCode)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(a => a.Country)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.IsDefault)
                
               .HasDefaultValue(false);

        // Soft delete
        builder.Property(a => a.IsDeleted)
               
               .HasDefaultValue(false);

    

        builder.HasQueryFilter(a => !a.IsDeleted);

        // Relationship: Address belongs to User (FK configured from UserConfiguration)
        builder.HasOne(a => a.User)
               .WithMany(u => u.Addresses)
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
