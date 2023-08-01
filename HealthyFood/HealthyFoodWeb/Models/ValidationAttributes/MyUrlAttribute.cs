using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models.ValidationAttributes
{
    public class MyUrlAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string imgPath)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"The {imgPath} must be URL"
                : ErrorMessage;
        }

        public override bool IsValid(object? url)
        {
            return Uri.IsWellFormedUriString((string?)url, UriKind.Absolute);
        }
    }
}
