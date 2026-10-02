using ConsoleDump;
using lab3.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab3
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();
            
            var customers = context.Customers
                .Where(c => c.Name.Contains("Tasnim"))
                .ToList()
                .Dump();

            var customerOrders = context.Orders
                .Include(o => o.Customer)
                .Select(o => new
                {
                    CustomerName = o.Customer.Name,
                    o.OrderDate,
                    Total = o.TotalAmount
                })
                .ToList()
                .Dump("Customer Orders: ");

            var expensive = context.Products
                .Where(p => p.Price > 100)
                .ToList()
                .Dump("All Expensive Products: ");

            var sales = context.Orders
                .GroupBy(o => o.Customer)
                .Select(g => new 
                {
                    CustomerId = g.Key.Id,
                    CustomerName = g.Key.Name,
                    Total = g.Sum(o => o.TotalAmount)
                })
                .ToList()
                .Dump("Total Amount for each Customer: ");
        }
    }
}