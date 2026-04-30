using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Carts.ViewModels
{
    public class CartItemVM
    {
        public int ProductId{ get; set; }
        public string ProductName{ get; set; } = string.Empty;
        public decimal UnitPrice {  get; set; }
        public int Quantity {  get; set; }
        public decimal LineTotal {  get; set; }
    }
}
