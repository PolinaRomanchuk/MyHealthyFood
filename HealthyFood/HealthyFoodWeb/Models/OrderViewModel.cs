using HealthyFoodWeb.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models
{
    public class OrderViewModel
    {
        [CustomRequired]
        public DateTime OrderTime { get; set; }

        [CustomRequired]
        public string Street { get; set; }
        [CustomRequired]
        public string House { get; set; }
        public string? Doorway { get; set; }
        public string? Floor { get; set; }

        [CustomRequired]
        public string Flat { get; set; }

        [MyPhoneValidation]
        [CustomRequired]
        public string PhoneNumber { get; set; }
        [CustomRequired]
        public string Name { get; set; }
        public string? Comment { get; set; }
        public string PaymentMethod { get; set; }
    }
}
