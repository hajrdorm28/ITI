using System;

namespace lab3.Entities
{
    public class CustomerProfile
    {
        // Use the same key as Customer (1-1)
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public string? Address { get; set; }
        public string? NationalId { get; set; }
    }
}
