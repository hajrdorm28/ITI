using lab3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab3.Data.Configration
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
            builder.HasData(
                new Order { Id = 1, CustomerId = 1, OrderDate = new DateTime(2024, 9, 1), TotalAmount = 150 },
                new Order { Id = 2, CustomerId = 2, OrderDate = new DateTime(2024, 9, 2), TotalAmount = 280 },
                new Order { Id = 3, CustomerId = 3, OrderDate = new DateTime(2024, 9, 3), TotalAmount = 200 },
                new Order { Id = 4, CustomerId = 4, OrderDate = new DateTime(2024, 9, 4), TotalAmount = 100 },
                new Order { Id = 5, CustomerId = 5, OrderDate = new DateTime(2024, 9, 5), TotalAmount = 500 },
                new Order { Id = 6, CustomerId = 6, OrderDate = new DateTime(2024, 9, 6), TotalAmount = 80 },
                new Order { Id = 7, CustomerId = 7, OrderDate = new DateTime(2024, 9, 7), TotalAmount = 120 },
                new Order { Id = 8, CustomerId = 8, OrderDate = new DateTime(2024, 9, 8), TotalAmount = 90 },
                new Order { Id = 9, CustomerId = 9, OrderDate = new DateTime(2024, 9, 9), TotalAmount = 250 },
                new Order { Id = 10, CustomerId = 10, OrderDate = new DateTime(2024, 9, 10), TotalAmount = 200 }
            );
        }
    }
}
