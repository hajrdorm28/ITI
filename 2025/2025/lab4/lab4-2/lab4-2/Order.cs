using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_2
{
    struct Order
    {
        public int Id;
        public string CustomerName;
        public double Amount;
        public OrderStatus Status;

        public Order(int id, string customerName, double amount, OrderStatus status)
        {
            Id = id;
            CustomerName = customerName;
            Amount = amount;
            Status = status;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Id: {Id}\n" +
                $"Customer Name: {CustomerName}\n" +
                $"Amount: {Amount}\n" +
                $"Statues: {Status}\n");
        }

        /*
         public override string ToString()
        {
            return $"Id: {Id}\n" +
                $"Customer Name: {CustomerName}\n" +
                $"Amount: {Amount}\n" +
                $"Statues: {Status}";
        }
        
        public void PrintInfo()
        {
            Console.WriteLine(ToString());
        }
        */
    }

}
