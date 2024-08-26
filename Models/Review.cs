using System.ComponentModel.DataAnnotations;

namespace StoreAPIWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
    }
}
