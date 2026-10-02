using lab3.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace lab3.Data 
{
   
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Categories> Categorys { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => base.OnConfiguring(optionsBuilder.UseSqlServer(@"Server=;Database=E-commerce;Trusted_Connection=True;Encrypt=False;"));
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
