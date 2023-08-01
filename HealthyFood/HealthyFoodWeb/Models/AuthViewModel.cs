using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models
{
    public class AuthViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
