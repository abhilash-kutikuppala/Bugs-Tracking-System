namespace BugsTrackingSystem.Models
{
    public class Bug
    {
        public int BugID { get; set; }
        public string Description { get; set; }
        public State BugState { get; set; }
        public int ProjectID { get; set; }
    }

    public enum State { Open, Working, Resolved }
}
