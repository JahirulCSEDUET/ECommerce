using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.Identity
{
    public  class ApplicationUser:IdentityUser
    {
        public string? FullName { get; set; }
        public string? ShippingAddress {  get; set; }
    }
}
