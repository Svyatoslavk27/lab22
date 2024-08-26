using System.ComponentModel.DataAnnotations;

namespace StoreAPIWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string InStock { get; set; }
        public int CategoryId { get; set; }

        // Navigation properties
        public virtual ICollection<Category>? Categories { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }

}
