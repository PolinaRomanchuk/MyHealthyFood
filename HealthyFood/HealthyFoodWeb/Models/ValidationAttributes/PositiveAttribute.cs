using System.ComponentModel.DataAnnotations;

namespace HealthyFoodWeb.Models.ValidationAttributes
{
    /// <summary>
    /// Work with int, long, float, double and decimal
    /// </summary>
    public class PositiveAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"The {name} must be more then 0"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            switch (value)
            {
                case int number:
                    return number > 0;
                case long number:
                    return number > 0;
                case float number:
                    return number > 0;
                case double number:
                    return number > 0;
                case decimal number:
                    return number > 0;
                default:
                    throw new Exception("Attribut on the wrong setting");
            }
        }
    }
}
