using System.Collections.Generic;

namespace stupid.Models
{
    public class Package : BaseEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public int cost { get; set; }
        public IEnumerable<Product> products { get; set; }
        // public string duration { get; set; } - Do we need this?
        //public string catagory { get; set; }
    }
}