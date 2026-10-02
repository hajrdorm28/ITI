namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeListViewModel
    {
        public List<EmployeeIndexViewModel> Employees { get; set; } = new List<EmployeeIndexViewModel>();

        public string SearchTerm { get; set; }

        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages => PageSize <= 0 ? 1 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
