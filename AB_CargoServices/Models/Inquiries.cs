using System.ComponentModel.DataAnnotations;

namespace AB_CargoServices.Models
{
    public class Inquiries
    {
        [Required]
        [Display(Name="Your Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name="Your Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(140, ErrorMessage = "Message should be maximumly 140 characters")]
        public string Message { get; set; }
    }
}