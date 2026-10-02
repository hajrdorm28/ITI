using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Human Resources", ManagerName = "Sara Ahmed" },
                new Department { Id = 2, Name = "Engineering", ManagerName = "Omar Khaled" },
                new Department { Id = 3, Name = "Marketing", ManagerName = "Laila Hassan" }
            );

            modelBuilder.Entity<Employee>().HasData(
                // Human Resources (DepartmentId = 1)
                new Employee { Id = 1, Name = "Ahmed Fathy", Age = 25, Salary = 12000, JobTitle = "HR Specialist", DepartmentId = 1 },
                new Employee { Id = 2, Name = "Mona Reda", Age = 30, Salary = 15000, JobTitle = "Recruiter", DepartmentId = 1 },

                // Engineering (DepartmentId = 2)
                new Employee { Id = 3, Name = "Youssef Adel", Age = 28, Salary = 22000, JobTitle = "Software Engineer", DepartmentId = 2 },
                new Employee { Id = 4, Name = "Nourhan Samir", Age = 24, Salary = 18000, JobTitle = "QA Engineer", DepartmentId = 2 },
                new Employee { Id = 5, Name = "Hassan Ali", Age = 35, Salary = 30000, JobTitle = "Senior Developer", DepartmentId = 2 },

                // Marketing (DepartmentId = 3)
                new Employee { Id = 6, Name = "Dina Mostafa", Age = 27, Salary = 16000, JobTitle = "Marketing Specialist", DepartmentId = 3 },
                new Employee { Id = 7, Name = "Karim Nabil", Age = 40, Salary = 25000, JobTitle = "Marketing Manager", DepartmentId = 3 }
            );
        }
    }
}
