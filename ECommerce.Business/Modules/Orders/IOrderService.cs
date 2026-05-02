using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Orders
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync( string userId, string shipAddress);
        Task<Order> GetByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetAllAsync();
        Task<IReadOnlyList<Order>> GetAllByUserIdAsync(string userId);
        Task UpdateAsync(Order order);
    }
}
