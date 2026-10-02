using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.Models; 

namespace NorthwindApp.Data
{
    public class NorthwindQueries
    {
        private readonly NorthwindContext _context;

        public NorthwindQueries(NorthwindContext context)
        {
            _context = context;
        }

        public void PrintExpensiveProducts()
        {
            var expensiveProducts = _context.Products
                .Where(p => p.UnitPrice > 50)
                .OrderByDescending(p => p.UnitPrice)
                .Select(p => new { p.ProductName, p.UnitPrice })
                .ToList();

            Console.WriteLine("Products priced over $50:");
            foreach (var p in expensiveProducts)
            {
                Console.WriteLine($"   {p.ProductName}: {p.UnitPrice:C}");
            }
        }

        public void PrintCustomerWithOrders(string customerId)
        {
            var customer = _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                Console.WriteLine($"No customer found with id '{customerId}'.");
                return;
            }

            Console.WriteLine($"Customer: {customer.CompanyName} ({customer.CustomerId})");
            Console.WriteLine($"Total orders: {customer.Orders.Count}");
            foreach (var order in customer.Orders.OrderByDescending(o => o.OrderDate))
            {
                Console.WriteLine($"   Order #{order.OrderId} placed on {order.OrderDate:d}");
            }
        }

        public void PrintEmployeeOrderCounts()
        {
            var employeeOrderCounts = _context.Employees
                .Select(e => new
                {
                    FullName = e.FirstName + " " + e.LastName,
                    OrderCount = e.Orders.Count
                })
                .OrderByDescending(e => e.OrderCount)
                .ToList();

            Console.WriteLine("Orders processed per employee:");
            foreach (var e in employeeOrderCounts)
            {
                Console.WriteLine($"   {e.FullName}: {e.OrderCount} order(s)");
            }
        }

        public void PrintCustomersSortedByName()
        {
            var sortedCustomers = _context.Customers
                .OrderBy(c => c.CompanyName)
                .Select(c => c.CompanyName)
                .ToList();

            Console.WriteLine("Customers sorted alphabetically:");
            foreach (var name in sortedCustomers)
            {
                Console.WriteLine($"   {name}");
            }
        }
    }
}
