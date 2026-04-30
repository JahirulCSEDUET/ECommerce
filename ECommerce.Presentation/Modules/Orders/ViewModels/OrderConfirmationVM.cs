using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders.ViewModels
{
    public class OrderConfirmationVM
    {
        public int OrderId {  get; set; }
        public DateTime Orderdate { get; set; }
        public decimal TotalAmount {  get; set; }
        public string Status {  get; set; } =string.Empty;
    }
}
