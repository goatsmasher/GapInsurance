namespace stupid.Models
{
    public class User : BaseEntity
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int admin { get; set; }
    }
}