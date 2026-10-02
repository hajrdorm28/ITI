using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindApp.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Column("CustomerID")]
        public string? CustomerId { get; set; }

        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }

        public string? ShipCity { get; set; }

        public string? ShipRegion { get; set; }

        public string? ShipPostalCode { get; set; }

        public string? ShipCountry { get; set; }

        public Customer? Customer { get; set; }

        public Employee? Employee { get; set; }
    }
}
