using System.ComponentModel.DataAnnotations;
using EmployeeManagementSystem.ViewModels;

namespace EmployeeManagementSystem.Validation
{
    public class MinimumAgeIfHighSalaryAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly decimal _salaryThreshold;

        public MinimumAgeIfHighSalaryAttribute(int minAge, double salaryThreshold)
        {
            _minAge = minAge;
            _salaryThreshold = (decimal)salaryThreshold;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is not EmployeeViewModel vm)
            {
                return ValidationResult.Success;
            }

            if (value is not int age)
            {
                return ValidationResult.Success;
            }

            if (vm.Salary > _salaryThreshold && age < _minAge)
            {
                var message = ErrorMessage ??
                    $"Employees earning more than {_salaryThreshold:N0} must be at least {_minAge} years old.";
                return new ValidationResult(message, new[] { nameof(EmployeeViewModel.Age) });
            }

            return ValidationResult.Success;
        }
    }
}
