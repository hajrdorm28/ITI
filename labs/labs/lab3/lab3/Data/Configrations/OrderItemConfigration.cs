using lab3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab3.Data.Configration
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => new {oi.OrderId, oi.ProductId});

            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(10, 2)");

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            builder.HasData(
                new OrderItem { OrderId = 1, ProductId = 1, Price = 100, Quantity = 1 },
                new OrderItem { OrderId = 2, ProductId = 3, Price = 200, Quantity = 1 },
                new OrderItem { OrderId = 2, ProductId = 4, Price = 80, Quantity = 1 },
                new OrderItem { OrderId = 3, ProductId = 5, Price = 120, Quantity = 1 },
                new OrderItem { OrderId = 4, ProductId = 2, Price = 100, Quantity = 1 },
                new OrderItem { OrderId = 5, ProductId = 10, Price = 500, Quantity = 1 },
                new OrderItem { OrderId = 6, ProductId = 7, Price = 80, Quantity = 1 },
                new OrderItem { OrderId = 7, ProductId = 8, Price = 120, Quantity = 1 },
                new OrderItem { OrderId = 8, ProductId = 6, Price = 90, Quantity = 1 },
                new OrderItem { OrderId = 9, ProductId = 9, Price = 250, Quantity = 1 },
                new OrderItem { OrderId = 10, ProductId = 3, Price = 200, Quantity = 1 }
            );
        }
    }
}
