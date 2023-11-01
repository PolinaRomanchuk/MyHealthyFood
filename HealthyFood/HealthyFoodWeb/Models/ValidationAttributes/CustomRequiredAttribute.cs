using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models.ValidationAttributes
{
    public class CustomRequiredAttribute : RequiredAttribute
    {
        public CustomRequiredAttribute()
        {
            ErrorMessage = "Поле обязательно для заполнения";
        }
    }
}
