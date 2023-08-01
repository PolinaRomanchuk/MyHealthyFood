using HealthyFoodWeb.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public IFormFile CoverFile { get; set; }

        //[Positive(ErrorMessage = "Everybody have to pay for the game")]
        [Positive]
        public decimal Price { get; set; }

        public int ActiveGamers { get; set; }

        public List<string> AvailableGenres { get; set; } = new List<string>();

        public List<string> Genres { get; set; } = new List<string>();
        public List<string> Friends { get; set; } = new List<string>();
    }
}
