namespace BugsTrackingSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalNoOfProjects { get; set; }
        public int TotalNoOfBugs { get; set; }
        public int TotalNoOfMessages { get; set; }
        public double BugResolutionRate { get; set; }
    }
}
