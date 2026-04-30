using ECommerce.Presentation.Modules.Carts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders.ViewModels
{
    public class CheckoutVM
    {
        public CartVM Cart { get; set; } = null!;

        [Required(ErrorMessage="Full Name is required!")]
        [StringLength(100)]
        [Display(Name="Full Name")]
        public string FullName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shipping Address is required!")]
        [StringLength(500)]
        [Display(Name = "Shipping Address")]
        public string ShipAddress { get; set; } = string.Empty;
    }
}
