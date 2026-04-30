using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Presentation.Modules.Account.ViewModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }= string.Empty;

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
