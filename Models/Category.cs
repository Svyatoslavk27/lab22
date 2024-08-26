using System.ComponentModel.DataAnnotations;

namespace StoreAPIWebApp.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
     
        public virtual ICollection<Product> Products { get; set; }

    }
}