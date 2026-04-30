using ECommerce.Business.Exceptions;
using ECommerce.Presentation.Modules.Categories.Interfaces;
using ECommerce.Presentation.Modules.Products;
using ECommerce.Presentation.Modules.Products.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductController : Controller
    {
        private readonly IProductViewModelProvider _productViewModelProvider;
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductViewModelProvider productViewModelProvider, ICategoryViewModelProvider categoryViewModelProvider, IWebHostEnvironment env)
        {
            _productViewModelProvider = productViewModelProvider;
            _categoryViewModelProvider = categoryViewModelProvider;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productViewModelProvider.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            var category = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.categories = new SelectList(category, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM productvm, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var Category = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(Category, "Id", "Name");
                return View(productvm);
            }
            productvm.ImagePath = await SaveProductImageAsync(ImageFile);
            try
            {
                await _productViewModelProvider.AddAsync(productvm);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputExceptions ex)
            {
                ModelState.AddModelError(nameof(ProductCreateVM.SKU), ex.Message);
                var Category = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(Category, "Id", "Name");
                return View(productvm);
            }
        }
        

        public async Task<IActionResult> Edit(int id)
        {
            var Category = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.Categories = new SelectList(Category, "Id", "Name");
            var product = await _productViewModelProvider.GetByIdAsync(id);

            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ProductEditVM productVM, IFormFile? ImageFile)
        {
            if(id != productVM.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var Category = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(Category, "Id", "Name");
                return View(productVM);
            }
            productVM.ImagePath = await SaveProductImageAsync(ImageFile, productVM.ImagePath);
            try
            {
                await _productViewModelProvider.UpdateAsync(productVM);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputExceptions ex)
            {
                
                ModelState.AddModelError(nameof(ProductEditVM.SKU), ex.Message);
                var Category = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(Category, "Id", "Name");
                return View(productVM);
            }
        }
        public async Task<IActionResult> Details(int id) {
            var Category = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.Categories = new SelectList(Category, "Id", "Name");
            var product = await _productViewModelProvider.GetByIdAsync(id);

            if (product == null) return NotFound();
            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Category = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.Categories = new SelectList(Category, "Id", "Name");
            var product = await _productViewModelProvider.GetByIdAsync(id);

            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            var prod = await _productViewModelProvider.GetByIdAsync(id);
            if(!string.IsNullOrEmpty (prod.ImagePath))
            {
                var oldFull = Path.Combine(_env.WebRootPath, prod.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFull))
                {
                    System.IO.File.Delete(oldFull);
                }
            }
            bool conf= await _productViewModelProvider.DeleteAsync(id);
            if (!conf)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task<string?> SaveProductImageAsync(IFormFile imageFile, string? oldPath = null)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return oldPath;
            }
            // extention validation 
            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !allowed.Contains(ext))
            {
                return oldPath;
            }
            //path define or create
            var dir = Path.Combine(_env.WebRootPath, "Images", "Products");
            Directory.CreateDirectory(dir);
            //Image name guid
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(dir, fileName);
            using (var steam = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(steam);
            }
            if (!string.IsNullOrEmpty(oldPath))
            {
                var oldFull = Path.Combine(_env.WebRootPath, oldPath.TrimStart('/'));
                if (System.IO.File.Exists(oldFull))
                {
                    System.IO.File.Delete(oldFull);
                }
            }
            return "/Images/Products/" + fileName;
        }
    }
}
