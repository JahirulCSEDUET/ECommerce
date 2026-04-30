using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; } 
        public string ApplicationUserId {  get; set; }
        public int ProductId {  get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public DateTime? AddedDate {  get; set; }

    }
}
