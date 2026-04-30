using ECommerce.Presentation.Modules.Carts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Carts
{
    public interface ICartViewModelProvider
    {
        CartVM GetCartVM();
        void AddItem(int productId, string productName, decimal unitPrice, int quantity=1);
        void UpdateQuantity(int productId, int Quantity);
        void RemoveItem(int productId);        
    }
}
