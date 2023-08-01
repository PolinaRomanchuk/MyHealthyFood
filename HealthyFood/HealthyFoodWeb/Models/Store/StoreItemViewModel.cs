using Data.Interface.Models;
using HealthyFoodWeb.Models.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models.Store
{
    public class StoreItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Positive]
        public decimal Price { get; set; }
        public string Img { get; set; }
        public string Manufacturer { get; set; }

        public List<SelectListItem>? AllManufacturers { get; set; }

    }
}