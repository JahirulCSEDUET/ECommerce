using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Orderdate {  get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShipAddress {  get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }

    }
}
