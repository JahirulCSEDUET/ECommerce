using AutoMapper;
using ECommerce.Business.Modules.Categories;
using ECommerce.Domain.Entities;
using ECommerce.Presentation.Modules.Categories.Interfaces;
using ECommerce.Presentation.Modules.Categories.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Categories
{
    public class CategoryViewModelProvider : ICategoryViewModelProvider
    {
        private readonly ICategoryService _categorysevice;
        private readonly IMapper _mapper;
        public CategoryViewModelProvider(ICategoryService categorysevice, IMapper mapper)
        {
            _categorysevice = categorysevice;
            _mapper =mapper;
        }

        public async Task<Category> AddAsync(CategoryCreateVM categoryVM)
        {
            //var category = new Category();
            //category.Name = categoryVM.Name;
            //category.Description = categoryVM.Description;
            Category category = _mapper.Map<Category>(categoryVM);
            await _categorysevice.AddAsync(category);
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categorysevice.GetByIdAsync(id);
            if(category == null) return false;
            return await _categorysevice.DeleteAsync(category);
        }

        public async Task<IReadOnlyList<CategoryListVM>> GetAllAsync()
        {
            var categoryList = await _categorysevice.GetAllAsync();
            //return categoryList.Select(c => new CategoryListVM
            //{
            //    Id = c.Id,
            //    Name = c.Name,
            //    Description = c.Description,
            //    CreateDate = c.CreateDate
            //}).ToList();
            var CateList = _mapper.Map<IReadOnlyList<CategoryListVM>>(categoryList);
            return CateList;
        }

        public async Task<CategoryEditVM> GetByIdAsync(int id)
        {
            var category= await _categorysevice.GetByIdAsync(id);
            if (category == null) return null;
            //var categoryVM = new CategoryEditVM
            //{
            //    Id = category.Id,
            //    Name = category.Name,
            //    Description = category.Description,
            //    CreateDate = category.CreateDate
            //};
            var categoryVM = _mapper.Map<CategoryEditVM>(category);
            return categoryVM;
        }

        public async Task UpdateAsync(CategoryEditVM categoryEditVM)
        {
            //var category = new Category();
            //category.Id = categoryEditVM.Id;
            //category.Name = categoryEditVM.Name;
            //category.Description = categoryEditVM.Description;
            //category.CreateDate = categoryEditVM.CreateDate;
            var category = _mapper.Map<Category>(categoryEditVM);
            await _categorysevice.UpdateAsync(category);
        }
    }
}
