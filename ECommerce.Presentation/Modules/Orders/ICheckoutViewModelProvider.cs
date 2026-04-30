using ECommerce.Presentation.Modules.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders
{
    public interface ICheckoutViewModelProvider
    {
        Task<CheckoutVM?> GetCheckOutViewModel(string userId);
        Task<OrderConfirmationVM?> PlaceOrderAsync(CheckoutVM model, string userId);
    }
}
