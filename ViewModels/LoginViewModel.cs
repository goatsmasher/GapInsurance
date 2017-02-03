using System.ComponentModel.DataAnnotations;

namespace stupid.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddressAttribute]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MinLength(8)]
        public string password { get; set; }
        public string ReturnUrl { get; set; }
    }
}