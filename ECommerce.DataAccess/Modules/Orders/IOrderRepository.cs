using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.Modules.Orders
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int orderId);
        Task<Order> AddAsync(Order order);
        Task UpdateAsync(Order order);
        //Task<bool> DeleteAsync(Order order);
    }
}
