namespace EmployeeManagementSystem.Services
{
    public class AppState
    {
        private int _requestCount = 0;

        public bool MaintenanceModeEnabled { get; set; } = false;

        public int RequestCount => _requestCount;

        public int IncrementRequestCount()
        {
            return Interlocked.Increment(ref _requestCount);
        }
    }
}
