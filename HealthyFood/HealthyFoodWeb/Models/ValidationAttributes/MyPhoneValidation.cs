using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HealthyFoodWeb.Models.ValidationAttributes
{
    public class MyPhoneValidation: ValidationAttribute
    {
        public override string FormatErrorMessage(string? phoneNumber)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Номер введен некорректно. Введите по образцу: 291231234"
                : ErrorMessage;
        }

        public override bool IsValid(object? phoneNumber)
        {
            if (phoneNumber != null)
            {
                return Regex.Match((string)phoneNumber, @"^(29|25|44|33)(\d{3})(\d{2})(\d{2})$").Success; 
            }
            return false;
        }
    }
}
