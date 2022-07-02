namespace Deloitte_Project.Models
{
    public class Session
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public ulong contact { get; set; }
        public string password { get; set; }
        public bool isDeleted { get; set; }
    }
}
