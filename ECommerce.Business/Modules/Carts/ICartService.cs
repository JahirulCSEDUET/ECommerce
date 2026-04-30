using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Carts
{
    public interface ICartService
    {
        Cart GetCart();
        void AddItem(int productId, string productName, decimal unitPrice, int quantity=1 );
        void UpdateQuatity(int  productId, int quantity);
        void RemoveItem(int productId);
        void ClearCart();
    }
}
