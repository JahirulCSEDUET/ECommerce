using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders.ViewModels
{
    public class OrderListVM
    {
        public int Id { get; set; }
        public DateTime Orderdate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShipAddress { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail {  get; set; }
        public ICollection<OrderItemListVM> OrderItems { get; set; } = new List<OrderItemListVM>();
    }
}
