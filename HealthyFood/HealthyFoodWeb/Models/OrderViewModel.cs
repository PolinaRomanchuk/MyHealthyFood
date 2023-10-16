using HealthyFoodWeb.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models
{
    public class OrderViewModel
    {
        public DateTime OrderTime { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string? Doorway { get; set; }
        public string? Floor { get; set; }
        public string Flat { get; set; }

        [MyPhoneValidation]
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public string PaymentMethod { get; set; }
    }
}
