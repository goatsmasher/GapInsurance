using System.ComponentModel.DataAnnotations;

namespace stupid.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public string name { get; set; }
        [Required]
        public int cost { get; set; }
        [Display(Name = "Description")]
        public string catagory { get; set; }
        public string description { get; set; }
        public string img_src { get; set; }
    }
}