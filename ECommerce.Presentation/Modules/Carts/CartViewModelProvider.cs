using ECommerce.Business.Modules.Carts;
using ECommerce.Presentation.Modules.Carts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Carts
{
    public class CartViewModelProvider : ICartViewModelProvider
    {
        private readonly ICartService _cartService;

        public CartViewModelProvider(ICartService cartService)
        {
            _cartService = cartService;
        }

        public void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1)=>_cartService.AddItem(productId, productName, unitPrice, quantity);

        public CartVM GetCartVM()
        {
            var cart = _cartService.GetCart();
            return new CartVM
            {
                Items = cart.Items.Select(i => new CartItemVM
                {
                    ProductId =i.
                    ProductId,  
                    ProductName =i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    LineTotal = i.LineTotal,
                }).ToList(),
                GrandTotal = cart.GrandTotal,
                TotalItems = cart.TotalItem
            };
        }

        public void RemoveItem(int productId)=>_cartService.RemoveItem(productId);
        public void UpdateQuantity(int productId, int Quantity)=>_cartService.UpdateQuatity(productId, Quantity);
    }
}
