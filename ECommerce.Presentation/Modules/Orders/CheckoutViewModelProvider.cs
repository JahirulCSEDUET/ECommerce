using ECommerce.Business.Modules.Carts;
using ECommerce.Business.Modules.Orders;
using ECommerce.DataAccess.Identity;
using ECommerce.Presentation.Modules.Carts;
using ECommerce.Presentation.Modules.Orders.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders
{
    public class CheckoutViewModelProvider : ICheckoutViewModelProvider
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private ICartViewModelProvider _cartViewModelProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutViewModelProvider(
            ICartViewModelProvider cartViewModelProvider,
            IOrderService orderService,
            ICartService cartService,
            UserManager<ApplicationUser> userManager)
        {
            _cartViewModelProvider = cartViewModelProvider;
            _orderService = orderService;
            _cartService = cartService;
            _userManager = userManager;
        }

        public async Task<CheckoutVM?> GetCheckOutViewModel(string userId)
        {
            var CartVM = _cartViewModelProvider.GetCartVM();
            if (CartVM == null) {
                return null;
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return new CheckoutVM { 
                Cart = CartVM ,
                Email = user.Email,
                FullName = user.FullName,
                ShipAddress = user.ShippingAddress
            };
        }

        public async Task<OrderConfirmationVM?> PlaceOrderAsync(CheckoutVM model, string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            user.ShippingAddress = model.ShipAddress;
            await _userManager.UpdateAsync(user);
            var order= await _orderService.PlaceOrderAsync( userId, model.ShipAddress);
            if (order == null) {
                return null; 
            }
            var confirm = new OrderConfirmationVM
            {
                Orderdate = order.Orderdate,
                OrderId = order.Id,
                Status = order.Status,
                TotalAmount = order.TotalAmount
            };
            
            return confirm;
        }
    }
}
