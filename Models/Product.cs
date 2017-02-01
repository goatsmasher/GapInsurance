namespace stupid.Models{
    public class Product : BaseEntity{
        public string name { get; set; }
        public int cost { get; set; }
        public string catagory { get; set; }
        public string description { get; set; }
        public string img_src { get; set; }
    }
}