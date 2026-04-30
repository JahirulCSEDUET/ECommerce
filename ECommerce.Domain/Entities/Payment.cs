using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string PayMethod {  get; set; }
        public DateTime PayDate { get; set; }
        public string TransId {  get; set; }
        public int OrderId {  get; set; }
        public Order? Order { get; set; }
    }
}
