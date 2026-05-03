using ECommerce.Business.Exceptions;
using ECommerce.Domain.Entities;
using ECommerce.Presentation.Modules.Categories.Interfaces;
using ECommerce.Presentation.Modules.Categories.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryViewModelProvider categoryViewModelProvider, ILogger<CategoryController> logger)
        {
            _categoryViewModelProvider = categoryViewModelProvider;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryListVM>>> GetAll()
        {
            var categories = await _categoryViewModelProvider.GetAllAsync();
            if (categories == null) return null;
            return Ok(categories);
        }
        [HttpGet("id")]
        public async Task<ActionResult<CategoryEditVM>> GetById(int id)
        {
            var category = await _categoryViewModelProvider.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody]CategoryCreateVM categoryVm)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Data are not binded successfully!");
                return BadRequest(ModelState);
            }
            try
            {
                var createdCategory = await _categoryViewModelProvider.AddAsync(categoryVm);
                _logger.LogInformation("A Category Created successfully");
                return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id },createdCategory);
            }
            catch(InvalidUserInputExceptions ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                _logger.LogInformation($"Unsuccessfull Creation: {ex.Message}");
                return BadRequest(ModelState);
            }
        }
        [HttpPut("id")]
        public async Task<ActionResult<CategoryEditVM>> Update([FromBody]CategoryEditVM updateCategory, int id)
        {
            var category = await _categoryViewModelProvider.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                category.Name = updateCategory.Name;
                category.Description = updateCategory.Description;
                await _categoryViewModelProvider.UpdateAsync(category);
                return Ok(category);
            }
            catch (InvalidUserInputExceptions ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("id")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var category = await _categoryViewModelProvider.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryViewModelProvider.DeleteAsync(id);
            return Ok();
        }
    }
}
