using ECommerce.Domain.Entities;
using ECommerce.Presentation.Modules.Categories.ViewModels;

namespace ECommerce.Presentation.Modules.Categories.Interfaces
{
    public interface ICategoryViewModelProvider
    {
        Task<IReadOnlyList<CategoryListVM>> GetAllAsync();
        Task<CategoryEditVM> GetByIdAsync(int id);
        Task<Category> AddAsync(CategoryCreateVM category);
        Task UpdateAsync(CategoryEditVM categoryEditVM);
        Task<bool> DeleteAsync(int id);
    }
}
