using Microsoft.EntityFrameworkCore;
using Lab_2.Models;

namespace Lab_2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, DepartmentName = "Human Resources", ManagerName = "Sara Ahmed" },
                new Department { Id = 2, DepartmentName = "Information Technology", ManagerName = "Omar Khaled" }
            );
        }
    }
}
