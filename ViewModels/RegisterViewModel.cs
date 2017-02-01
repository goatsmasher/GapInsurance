using System.ComponentModel.DataAnnotations;

namespace stupid.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 letters long")]
        public string first_name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 letters long")]
        public string last_name { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password must be length of 8: Include 1 Upper, 1 Lower, and 1 Middle-cased letters as well as the number of broken bones you have had in your life.")]
        public string password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [CompareAttribute("password", ErrorMessage = "Your passwords do not match")]
        public string compare { get; set; }
        [Required]
        [EmailAddressAttribute]
        [Display(Name = "Email")]
        public string email { get; set; }
    }
}