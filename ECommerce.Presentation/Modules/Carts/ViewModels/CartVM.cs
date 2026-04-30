using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Carts.ViewModels
{
    public class CartVM
    {
        public IReadOnlyCollection<CartItemVM> Items { get; set; } = Array.Empty<CartItemVM>();
        public decimal GrandTotal { get; set; }
        public int TotalItems {  get; set; }
    }
}
