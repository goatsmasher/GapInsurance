using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace stupid.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public string name { get; set; }
        [Display(Name = "Cost")]
        [Required]
        [Display(Name = "Cost")]
        public int cost { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile img_src { get; set; }
    }
}