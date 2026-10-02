using lab3.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab3.Data.Configration
{
    public class CustomerConfigration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //builder.HasData(
            //    new Customer { 
            //        Id = 1, 
            //        Name = "Tasnim", 
            //        Phone = "01050788567", 
            //        BirthDate = new DateTime(2006, 4, 1) }
            //    );

            builder.HasData(
                 new Customer { Id = 1, Name = "Tasnim", Phone = "010001", Email = "tasnim@mail.com", BirthDate = new DateTime(2006, 4, 1) },
                 new Customer { Id = 2, Name = "Sarah", Phone = "010002", Email = "sarah@mail.com", BirthDate = new DateTime(2005, 3, 10) },
                 new Customer { Id = 3, Name = "Ali", Phone = "010003", Email = "ali@mail.com", BirthDate = new DateTime(2004, 7, 5) },
                 new Customer { Id = 4, Name = "Laila", Phone = "010004", Email = "laila@mail.com", BirthDate = new DateTime(2002, 12, 21) },
                 new Customer { Id = 5, Name = "Hossam", Phone = "010005", Email = "hossam@mail.com", BirthDate = new DateTime(2000, 11, 1) },
                 new Customer { Id = 6, Name = "Nada", Phone = "010006", Email = "nada@mail.com", BirthDate = new DateTime(1998, 8, 13) },
                 new Customer { Id = 7, Name = "Youssef", Phone = "010007", Email = "youssef@mail.com", BirthDate = new DateTime(1999, 5, 5) },
                 new Customer { Id = 8, Name = "Dina", Phone = "010008", Email = "dina@mail.com", BirthDate = new DateTime(2001, 6, 15) },
                 new Customer { Id = 9, Name = "Mona", Phone = "010009", Email = "mona@mail.com", BirthDate = new DateTime(2003, 2, 22) },
                 new Customer { Id = 10, Name = "Khaled", Phone = "010010", Email = "khaled@mail.com", BirthDate = new DateTime(2004, 10, 30) }
            );
        }
    }
}
