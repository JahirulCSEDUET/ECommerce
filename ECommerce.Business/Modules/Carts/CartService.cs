using ECommerce.DataAccess.Modules.Carts;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            var cart = _cartRepository.GetCarts();
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                 cart.Items.Add(new CartItem { 
                     ProductId = productId, 
                     ProductName = productName, 
                     UnitPrice = unitPrice, 
                     Quantity = quantity 
                 });
            }
            _cartRepository.SaveCart(cart);
        }

        public void ClearCart()
        {
            _cartRepository.Clear();
        }

        public Cart GetCart() => _cartRepository.GetCarts();

        public void RemoveItem(int productId)
        {
            var cart = _cartRepository.GetCarts();
            var item = cart.Items.FirstOrDefault(i=> i.ProductId == productId);
            if (item != null) {
                cart.Items.Remove(item);
                _cartRepository.SaveCart(cart);
            }
        }

        public void UpdateQuatity(int productId, int quantity)
        {
            var cart = _cartRepository.GetCarts();
            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                return;
            }
            if (quantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;

            }
            _cartRepository.SaveCart(cart);
        }
    }
}
