using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // One-to-Many
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
