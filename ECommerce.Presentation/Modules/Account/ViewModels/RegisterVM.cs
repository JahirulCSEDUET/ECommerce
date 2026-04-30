using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Presentation.Modules.Account.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage ="The {0} must be at least {2} character.", MinimumLength =2)]
        [Display(Name = "Full Name")]
        public string FullName {  get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} character.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage ="Password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
