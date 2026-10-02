namespace EmployeeManagementSystem.ViewModels
{
    public class DepartmentListViewModel
    {
        public List<DepartmentIndexViewModel> Departments { get; set; } = new List<DepartmentIndexViewModel>();
        public List<string> RecentlyVisited { get; set; } = new List<string>();
        public int? LastVisitedDepartmentId { get; set; }
    }
}
