using ECommerce.Presentation.Modules.Carts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartViewModelProvider _cartViewModelProvider;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartViewModelProvider cartViewModelProvider, ILogger<CartController> logger)
        {
            _cartViewModelProvider = cartViewModelProvider;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Cart Page Accessed");

            var cart = _cartViewModelProvider.GetCartVM();
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            _logger.LogDebug($"Add to cart: product name {productName}, product id  {productId}"); 

            if (quantity < 1) quantity = 1;
            _cartViewModelProvider.AddItem(productId, productName, unitPrice, quantity);
            TempData["CartMessage"] = $"Added {productName} to cart";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int productId, int Quantity)
        {
            _cartViewModelProvider.UpdateQuantity(productId,Quantity);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId) {
            _cartViewModelProvider.RemoveItem(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
