using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Department Name must be between 3 and 30 characters.")]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Manager Name is required.")]
        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }
    }
}
