using lab3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab3.Data.Configration
{
    public class CustomerProfileConfigration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            // Primary key = CustomerId (shared FK/PK)
            builder.HasKey(cp => cp.CustomerId);

            // 1-1 relationship
            builder.HasOne(cp => cp.Customer)
                   .WithOne(c => c.Profile)
                   .HasForeignKey<CustomerProfile>(cp => cp.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(cp => cp.Address)
                   .HasMaxLength(200);

            builder.Property(cp => cp.NationalId)
                   .HasMaxLength(20);

            // Seed a few profiles (ensure matching customers exist)
            builder.HasData(
                new CustomerProfile { CustomerId = 1, Address = "Cairo, Egypt", NationalId = "29804151234567" },
                new CustomerProfile { CustomerId = 2, Address = "Giza, Egypt", NationalId = "30003101234567" },
                new CustomerProfile { CustomerId = 3, Address = "Alexandria, Egypt", NationalId = "29907051234567" }
            );
        }
    }
}
