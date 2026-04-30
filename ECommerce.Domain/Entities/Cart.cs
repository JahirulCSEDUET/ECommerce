using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal GrandTotal => Items.Sum(i=> i.LineTotal);
        public int TotalItem => Items.Sum(i => i.Quantity);
    }
}
