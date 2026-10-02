using System;

namespace lab4_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an array of 3 orders.
            var orders = new Order[]
            {
                new Order(101, "Tasnim Alkhatib", 1200, OrderStatus.Pending),
                new Order(102, "Marwa Ahmed", 650, OrderStatus.Processing),
                new Order(103, "Ahmed Ali", 499.99, OrderStatus.Shipped)
            };

            // Update the status of one order.
            var manage = new OrderManager();
            manage.UpdateStatus(ref orders[1], OrderStatus.Delivered);

            // Get order stats (total count and total amount).
            manage.GetOrderStats(orders, out int totalOrders, out double totalAmount);

            Console.WriteLine($"Total Orders: {totalOrders}");
            Console.WriteLine($"Total Amount: {totalAmount}");

            Console.WriteLine();

            //  Print all orders.
            foreach (var order in orders)
                manage.PrintOrder(order);

        }
    }
}