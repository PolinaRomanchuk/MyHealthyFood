using HealthyFoodWeb.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Positive]
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile ImgUrlFile { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<string> AvailableTags { get; set; } = new List<string>();
        public int Quantity { get; set; }
        public int TotalQuantity { get; set; }
    }
}
