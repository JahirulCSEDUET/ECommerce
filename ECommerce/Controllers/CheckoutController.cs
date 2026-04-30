using ECommerce.Presentation.Modules.Orders;
using ECommerce.Presentation.Modules.Orders.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutViewModelProvider _checkoutViewModelProvider;

        public CheckoutController(ICheckoutViewModelProvider checkoutViewModelProvider)
        {
            _checkoutViewModelProvider = checkoutViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId)) {
                return Challenge();
            }
            var model = await _checkoutViewModelProvider.GetCheckOutViewModel(userId);
            if (model == null) {
                TempData["Message"] = "Your Cart is empty";
                return RedirectToAction("Index", "Cart");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(CheckoutVM model)
        {
            ModelState.Remove("Cart");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }
            if (ModelState.IsValid)
            {
             
                var confirmation = await _checkoutViewModelProvider.PlaceOrderAsync(model, userId);
                if (confirmation == null)
                {
                    TempData["Message"] = "Could not Place Order. Cart may be empty.";
                    return RedirectToAction("Index", "Cart");
                }
                return RedirectToAction(nameof(Confimation), new {id = confirmation.OrderId});
            }
            
            var fresh =await _checkoutViewModelProvider.GetCheckOutViewModel(userId);
            if (fresh != null)
            {
                model.Cart = fresh.Cart;
            }
            return View(model);
            
        }
        public IActionResult Confimation(int id)
        {
            return View(id);
        }
    }
}
