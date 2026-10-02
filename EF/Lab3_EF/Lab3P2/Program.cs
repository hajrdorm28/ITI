using NorthwindApp.Data;
using NorthwindApp.Models;

namespace Lab3P2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new NorthwindContext();
            var queries = new NorthwindQueries(context);

            queries.PrintExpensiveProducts();
            Console.WriteLine();

            queries.PrintCustomersSortedByName();
            Console.WriteLine();

            queries.PrintEmployeeOrderCounts();
            Console.WriteLine();

            queries.PrintCustomerWithOrders("ALFKI");
        }
    }
}
