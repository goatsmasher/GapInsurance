using System.ComponentModel.DataAnnotations;

namespace stupid.ViewModels
{
    public class ProductViewModel
    {

        public string name { get; set; }

        public string description { get; set; }

        public string cost { get; set; }

        public string category { get; set; }

        public string img_src {get; set; }
    }
}