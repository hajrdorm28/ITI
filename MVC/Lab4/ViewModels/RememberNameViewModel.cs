using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.ViewModels
{
    public class RememberNameViewModel
    {
        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "User Name must be between 2 and 50 characters.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
