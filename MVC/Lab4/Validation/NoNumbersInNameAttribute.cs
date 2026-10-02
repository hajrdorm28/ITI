using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EmployeeManagementSystem.Validation
{
    public class NoNumbersInNameAttribute : ValidationAttribute
    {
        public NoNumbersInNameAttribute()
        {
            ErrorMessage = "Name must not contain numbers.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string name || string.IsNullOrEmpty(name))
            {
                return ValidationResult.Success;
            }

            if (Regex.IsMatch(name, @"\d"))
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
