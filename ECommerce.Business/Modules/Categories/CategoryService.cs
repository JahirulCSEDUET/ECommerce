using ECommerce.Business.Exceptions;
using ECommerce.DataAccess.Modules.Categories;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> AddAsync(Category category)
        {

            category.CreateDate= DateTime.Now;
            bool exist = await _categoryRepository.ExistsByNameAsync(category.Name);
            if (exist)
            {
                throw new InvalidUserInputExceptions("A Category with this name is already Exist!");
            }
            return await _categoryRepository.AddAsync(category);
            
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            return await _categoryRepository.DeleteAsync(category);
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Category category)
        {
            bool exist = await _categoryRepository.ExistsByNameAsync(category.Name, category.Id);
            if (exist)
            {
                throw new InvalidUserInputExceptions("A Category with this name is already Exist!");
            }
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
