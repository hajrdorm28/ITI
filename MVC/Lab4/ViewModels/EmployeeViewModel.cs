using System.ComponentModel.DataAnnotations;
using EmployeeManagementSystem.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [NoNumbersInName]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(20, 60, ErrorMessage = "Age must be between 20 and 60.")]
        [MinimumAgeIfHighSalary(22, 20000, ErrorMessage = "If salary is greater than 20,000, age must be at least 22.")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(5000, 50000, ErrorMessage = "Salary must be between 5000 and 50000.")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Job Title is required.")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
    }
}
