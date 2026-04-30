using AutoMapper;
using ECommerce.Business.Modules.Orders;
using ECommerce.DataAccess.Identity;
using ECommerce.Presentation.Modules.Orders.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders
{
    public class OrderViewModelProvider : IOrderViewModelprovider
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _user;

        public OrderViewModelProvider(IOrderService orderService, IMapper mapper, UserManager<ApplicationUser> user)
        {
            _orderService = orderService;
            _mapper = mapper;
            _user = user;
        }

        public async Task<IReadOnlyList<OrderListVM>> GetAllAsync()
        {
            var orders = await _orderService.GetAllAsync();
            if (orders == null) {
                return null;
            }
            
            return _mapper.Map<IReadOnlyList<OrderListVM>>(orders);
            
        }

        public async Task<OrderListVM> GetByIdAsync(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            var user = await _user.FindByIdAsync(order.ApplicationUserId);
            var orderVM =  _mapper.Map<OrderListVM>(order);
            orderVM.CustomerName = user.FullName;
            orderVM.CustomerEmail = user.Email;
            return orderVM;
        }

        public async Task UpdateAsync( int id, string status)
        {
            var order = await _orderService.GetByIdAsync(id);
            order.Status = status;
            await _orderService.UpdateAsync(order);
        }
    }
}
