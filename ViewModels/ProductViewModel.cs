using System.ComponentModel.DataAnnotations;

namespace stupid.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Cost")]
        public int cost { get; set; }
        [Display(Name = "Category")]
        public string category { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Image Source")]
        public string img_src { get; set; }
    }
}