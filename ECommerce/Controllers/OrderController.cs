using ECommerce.Presentation.Modules.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderViewModelprovider _orderViewModelprovider;

        public OrderController(IOrderViewModelprovider orderViewModelprovider)
        {
            _orderViewModelprovider = orderViewModelprovider;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }
            var orders = await _orderViewModelprovider.GetAllByUserIdAsync(userId);
            return View(orders);
        }
        public async Task<IActionResult> Details(int id) {
            var order=await _orderViewModelprovider.GetByIdAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Challenge();
            }
            if(userId != order.ApplicationUserId)
            {
                return NotFound();
            }
            return View(order);
        }

    }
}
