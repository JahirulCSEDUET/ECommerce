using ECommerce.Business.Modules.Carts;
using ECommerce.DataAccess.Modules.Carts;
using ECommerce.DataAccess.Modules.Orders;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;
         
        public OrderService(IOrderRepository orderRepository, ICartService cartReposioty)
        {
            _orderRepository = orderRepository;
            _cartService = cartReposioty;
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }
        public async Task<IReadOnlyList<Order>> GetAllByUserIdAsync(string userId)
        {
            return await _orderRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> PlaceOrderAsync(string userId, string shipAddress)
        {
            var cart = _cartService.GetCart();
            if (cart == null || cart.Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is Empty!");
            }
            
            var order = new Order
            {
                ApplicationUserId = userId,
                Orderdate = DateTime.Now,
                TotalAmount = cart.GrandTotal,
                Status = "Pending",
                ShipAddress = shipAddress
            };
            foreach (var item in cart.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Subtotal = item.LineTotal
                });
            }
            await _orderRepository.AddAsync(order);
            _cartService.ClearCart();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }
    }
}
