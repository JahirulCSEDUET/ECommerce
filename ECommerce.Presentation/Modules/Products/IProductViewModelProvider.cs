using ECommerce.Presentation.Modules.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Products
{
    public interface IProductViewModelProvider
    {
        Task<IReadOnlyList<ProductListVM>> GetAllAsync();
        Task<ProductEditVM> GetByIdAsync(int id);
        Task AddAsync(ProductCreateVM productVM);
        Task UpdateAsync(ProductEditVM productVM);
        Task <bool> DeleteAsync(int id);
    }
}
