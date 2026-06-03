using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;

namespace ShopApi.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // 1. Table name
         builder.ToTable("Products");

        // 2. Primary key
         builder.HasKey(p => p.Id);

        // 3. Name: required, max length 200
             builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);

        // 4. Description: required, max length 2000
        builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(2000);

        // 5. Price: precision 18, scale 2
        //    Hint: look up HasPrecision(precision, scale)
        builder.Property(p => p.Price)                           
                .HasPrecision(18, 2);

        // 6. ImageUrl: required, max length 500
               builder.Property(p => p.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500);

        // 7. Relationship to Category:
        //    - Product has one Category
        //    - Category has many Products
        //    - FK is CategoryId
        //    - On delete: Restrict
                builder.HasOne(p => p.Category)
                .WithMany(c=>c.Products)   
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        // 8. Index on CategoryId (optional — EF adds one for the FK automatically,
        //    but let's add it explicitly for learning):
        builder.HasIndex(p => p.CategoryId);

        // 9. Soft delete
        builder.Property(p => p.IsDeleted)
              
               .HasDefaultValue(false);
 

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}