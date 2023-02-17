namespace BugsTrackingSystem.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Text { get; set; }
        public bool Flag{ get; set; }
        public int BugID { get; set; }
    }
}
