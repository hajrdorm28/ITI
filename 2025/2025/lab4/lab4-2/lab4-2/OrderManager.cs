using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_2
{
    class OrderManager
    {
        public void UpdateStatus(ref Order order, OrderStatus newStatus)
        {
            order.Status = newStatus;
        }

        public void GetOrderStats(Order[] orders, out int totalOrders, out double totalAmount)
        {
            totalOrders = 0;
            totalAmount = 0;

            if (orders != null)
            {
                foreach (Order order in orders)
                {
                    totalOrders++;
                    totalAmount += order.Amount;
                }
            }
            return;
        }

        public void PrintOrder(in Order order)
        {
            order.PrintInfo();
        }
    }

}
