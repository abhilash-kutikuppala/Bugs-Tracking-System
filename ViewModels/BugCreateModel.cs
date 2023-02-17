using BugsTrackingSystem.Models;

namespace BugsTrackingSystem.ViewModels
{
    public class BugCreateModel
    {
        public int BugID { get; set; }
        public string Description { get; set; }
        public int ProjectID { get; set; }
    }

    
}
