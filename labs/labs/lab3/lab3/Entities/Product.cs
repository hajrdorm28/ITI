using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        
        public int CategoryId { get; set; }
        public Categories ?Category { get; set; }

        // Many-to-Many
        public ICollection<OrderItem> ?OrderItems { get; set; }
    }
}
