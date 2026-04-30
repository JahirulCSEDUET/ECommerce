using ECommerce.Business.Exceptions;
using ECommerce.Presentation.Modules.Categories.Interfaces;
using ECommerce.Presentation.Modules.Categories.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    //[Authorize(Policy ="FullNameOnly")]
    public class CategoryController : Controller
    {
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;

        public CategoryController(ICategoryViewModelProvider categoryViewModelProvider)
        {
            _categoryViewModelProvider = categoryViewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _categoryViewModelProvider.GetAllAsync();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM categoryCreateVM)
        {
            if (!ModelState.IsValid) 
            {
                return View(categoryCreateVM);
            }
            try
            {
                await _categoryViewModelProvider.AddAsync(categoryCreateVM);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputExceptions ex)
            {
                ModelState.AddModelError(nameof(CategoryCreateVM.Name), ex.Message);
                return View(categoryCreateVM);
            }
        }
        public async Task<IActionResult> Edit(int id) 
        {
            //throw new Exception();
            var cat = await _categoryViewModelProvider.GetByIdAsync(id);
            if (cat == null)
            {
                //return View("Error");
                return NotFound();
            }
            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryEditVM categoryEditVM) {
            if (!ModelState.IsValid) 
            { 
                return View(categoryEditVM); 
            }
            if (id != categoryEditVM.Id)
            {
                return NotFound();
            }
            try
            {
                await _categoryViewModelProvider.UpdateAsync(categoryEditVM);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputExceptions ex)
            {
                ModelState.AddModelError(nameof(CategoryEditVM.Name), ex.Message);
                return View(categoryEditVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _categoryViewModelProvider.GetByIdAsync (id);
            if(viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id) {
            var viewModel = await _categoryViewModelProvider.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            bool isDelete = await _categoryViewModelProvider.DeleteAsync(id);
            if (!isDelete)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _categoryViewModelProvider.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
    }
}
