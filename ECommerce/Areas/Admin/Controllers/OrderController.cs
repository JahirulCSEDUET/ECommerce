using ECommerce.Domain.Entities;
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
            var order =await _orderViewModelProvider.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsAsync(int id, bool status) 
        {
            if (!ModelState.IsValid)
            {
                var order = await _orderViewModelProvider.GetByIdAsync(id);
                return View(order);
            }
            var orde = await _orderViewModelProvider.GetByIdAsync(id);
            if (orde == null)
            {
                return NotFound();
            }
            await _orderViewModelProvider.UpdateAsync(id, status);
            return RedirectToAction(nameof(SuccessMessage), new {id = orde.Id});
        }
        public IActionResult SuccessMessage(int id)
        {
            return View(id);
        }

    }
}
