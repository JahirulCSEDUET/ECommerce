using ECommerce.Domain.Entities;

namespace ECommerce.DataAccess.Modules.Categories
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id);
        Task <IReadOnlyList<Category>> GetAllAsync();
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(Category category);
        Task<Category> AddAsync(Category category);
        Task<bool> ExistsByNameAsync(string name, int? excludedId=null);
    }
}
