using ECommerce.Presentation.Modules.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderViewModelprovider _orderViewModelProvider;

        public OrderController(IOrderViewModelprovider orderViewModelProvider)
        {
            _orderViewModelProvider = orderViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderViewModelProvider.GetAllAsync();
            return View(orders);
        }
        public async Task<IActionResult> Details(int id) 
        {
            var orders =await _orderViewModelProvider.GetByIdAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

    }
}
