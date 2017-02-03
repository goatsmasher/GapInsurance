using System.Collections.Generic;

namespace stupid.Models
{
    public class Cart : BaseEntity
    {
        public IEnumerable<Product> products { get; set; }
        public User user { get; set; }
        public int Users_id {get; set;}
        public int Products_id {get; set;}
        public int quantity {get; set;}
    }
}