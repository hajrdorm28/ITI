using lab3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab3.Data.Configration
{
    public class CategoriesConfigration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new Categories { Id = 1, Name = "Skincare" },
                new Categories { Id = 2, Name = "Makeup" },
                new Categories { Id = 3, Name = "Haircare" },
                new Categories { Id = 4, Name = "Fragrance" },
                new Categories { Id = 5, Name = "Suncare" },
                new Categories { Id = 6, Name = "Body Care" },
                new Categories { Id = 7, Name = "Tools" },
                new Categories { Id = 8, Name = "Sets" },
                new Categories { Id = 9, Name = "Men's Care" },
                new Categories { Id = 10, Name = "Kids Care" }
            );
        }
    }
}

