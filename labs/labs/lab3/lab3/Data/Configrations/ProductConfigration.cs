using lab3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab3.Data.Configration
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasData(
                new Product { Id = 1, Name = "Cleanser", Price = 100m, CategoryId = 1 },
                new Product { Id = 2, Name = "Toner", Price = 80m, CategoryId = 1 },
                new Product { Id = 3, Name = "Foundation", Price = 200m, CategoryId = 2 },
                new Product { Id = 4, Name = "Lipstick", Price = 150m, CategoryId = 2 },
                new Product { Id = 5, Name = "Shampoo", Price = 120m, CategoryId = 3 },
                new Product { Id = 6, Name = "Perfume", Price = 300m, CategoryId = 4 },
                new Product { Id = 7, Name = "Sunscreen", Price = 90m, CategoryId = 5 },
                new Product { Id = 8, Name = "Body Lotion", Price = 110m, CategoryId = 6 },
                new Product { Id = 9, Name = "Brush Set", Price = 250m, CategoryId = 7 },
                new Product { Id = 10, Name = "Gift Box", Price = 500m, CategoryId = 8 }
            );
        }
    }
}
