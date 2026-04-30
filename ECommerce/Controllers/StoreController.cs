using ECommerce.Presentation.Modules.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductViewModelProvider _productViewModelProvider;

        public StoreController(IProductViewModelProvider productViewModelProvider)
        {
            _productViewModelProvider = productViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productViewModelProvider.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Details(int id) {
            var product = await _productViewModelProvider.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
