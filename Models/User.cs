namespace Gp7_CA.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public double completionTime { get; set; }
        public bool isPaidUser { get; set; }
    }
}
