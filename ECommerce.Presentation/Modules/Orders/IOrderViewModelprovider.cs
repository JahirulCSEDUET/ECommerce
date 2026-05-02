using ECommerce.Presentation.Modules.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders
{
    public interface IOrderViewModelprovider
    {
        Task<IReadOnlyList<OrderListVM>> GetAllAsync();
        Task<IReadOnlyList<OrderListVM>> GetAllByUserIdAsync(string userId);
        Task<OrderListVM> GetByIdAsync(int id);
        Task UpdateAsync(int id, bool status);
    }
}
