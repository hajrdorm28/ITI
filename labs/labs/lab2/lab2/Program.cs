using ConsoleDump;
using Infra.Entities;
using Infra.Repositories;

namespace lab2
{
    public class Program
    {
        static CustomerRepository _customerRepo = new CustomerRepository();
        static ProductRepository _productRepo = new ProductRepository();
        static OrderRepository _orderRepo = new OrderRepository();
        static OrderItemRepository _orderItemRepo = new OrderItemRepository();
        
        static void Main(string[] args)
        {

            #region Task 1: Find Active Customers
            var activeCustomers = _customerRepo.GetAll()
                .Where(c => c.IsActive)
                .Dump("Active Customers: ");
            #endregion

            #region Task 2: Get Total Sales per Customer 
            var totalSales = _customerRepo.GetAll()
                    .GroupJoin(
                    _orderRepo.GetAll(),
                    customer => customer.Id,
                    order => order.CustomerId,
                    (customer, orderSum) => new
                    {
                        CustomerId = customer.Id,
                        totalSales = orderSum.Sum(order => order.TotalAmount)
                    })
                    .Dump("Total Sales per Customer");

            #endregion

            #region Task 3: Top 3 Customers by Loyalty Points 
            var top3Customers = _customerRepo.GetAll()
                    .OrderByDescending(c => c.LoyaltyPoints)
                    .Take(3)
                    .Dump("Top 3 Customers by Loyalty Points: ");
            #endregion

            #region Task 4: Group Products by Category 
            var productsByCategory = _productRepo.GetAll()
                    .GroupBy(p => p.Category)
                    .OrderBy(g => g.Key)
                    .SelectMany(g => g
                        .OrderBy(p => p.Name)
                        .Select(p => new { Category = g.Key, Product = p.Name }))
                    .Dump("Group Products by Category: "); 


            var productsByCategory2 = _productRepo.GetAll()
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Category = g.Key,
                    Product = string.Join(", ", g.OrderBy(p => p.Name).Select(p => p.Name))
                })
                .Dump("Group Products by Category: ");
            #endregion

            #region Task 5: Orders with Total Over $200  
            var OrdersOver200 = _orderRepo.GetAll()
            .Where(o => o.TotalAmount > 200m)
            .Select(o => new { o.Id, o.CustomerId, o.TotalAmount })
            .Dump("Orders with Total Over $200: ");
            #endregion

            #region Task 6: Products with Low Stock 
            var productsWithLowStock = _productRepo.GetAll()
                .Where(p => p.StockQuantity < 5)
                .Dump("Products with Low Stock: ");
            #endregion

            #region Task 7: Latest 5 Orders 
            var latestOrders = _orderRepo.GetAll()
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Dump("Latest 5 Orders: ");
            #endregion

            #region Task 8: Orders and Customer Info 
            var ordersAndCustomers = _customerRepo.GetAll()
                .Join(
                    _orderRepo.GetAll(),
                    customer => customer.Id,
                    order => order.CustomerId,
                    (customer, order) => $"{customer.Name} placed an order of {order.TotalAmount:F2} on {order.OrderDate:yyyy-MM-dd}")
                .Dump("Orders and Customer Info");
            #endregion

            #region Task 9: Order Count per Customer 
            var OrderCount = _customerRepo.GetAll()
                .GroupJoin(
                    _orderRepo.GetAll(),
                    customer => customer.Id,
                    order => order.CustomerId,
                (customer, orderCount) => new { customer.Name, Count = orderCount.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => $"{x.Name} - {x.Count} orders")
                .Dump("Order Count per Customer: ");
            #endregion

            #region Task 10: Calculate Average Product Price per Category 
            var avgPricePerCategory = _productRepo.GetAll()
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .Select(g => $"{g.Key} - ${g.Average(p => p.Price):F2}")
                .Dump("Calculate Average Product Price per Category: ");
            #endregion
        }
    }
}
