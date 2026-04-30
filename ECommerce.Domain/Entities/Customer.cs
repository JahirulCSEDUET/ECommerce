using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<ShoppingCart> ShoppingCarts { get; set;} = new List<ShoppingCart>();

    }
}
