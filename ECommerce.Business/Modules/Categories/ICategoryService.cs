using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Categories
{
    public interface ICategoryService
    {
        //Task<bool> CreateCategoryAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(Category category);
        Task<Category> AddAsync(Category category);
    }
}
