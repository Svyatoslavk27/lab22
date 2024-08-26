using System.ComponentModel.DataAnnotations;

namespace StoreAPIWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
    }
}