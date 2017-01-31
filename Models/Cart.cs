using System.Collections.Generic;

namespace stupid.Models
{
    public class Cart : BaseEntity
    {
        public IEnumerable<Product> products { get; set; }
        public User user { get; set; }
    }
}