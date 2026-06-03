using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;

namespace ShopApi.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // 1. Table name
            builder.ToTable("Categories");

            // 2. Primary key
            builder.HasKey(p => p.Id);

            // 3. Name: required, max length 200
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Slug)
                    .IsRequired()
                    .HasMaxLength(120);

            builder.HasIndex(p => p.Slug).IsUnique();

            // Soft delete
            builder.Property(p => p.IsDeleted)
                   
                   .HasDefaultValue(false);

       

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
