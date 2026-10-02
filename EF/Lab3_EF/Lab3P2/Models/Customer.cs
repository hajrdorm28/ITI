using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindApp.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [Column("CustomerID")]
        [MaxLength(5)]
        public string CustomerId { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string? ContactName { get; set; }

        public string? ContactTitle { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? Phone { get; set; }

        public string? Fax { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
