using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [Display(Name = "Department Name")]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Manager Name is required")]
        [Display(Name = "Manager Name")]
        [StringLength(100)]
        public string ManagerName { get; set; } = string.Empty; 
        public ICollection<Employee>? Employees { get; set; }
    }
}
