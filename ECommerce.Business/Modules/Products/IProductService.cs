using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Products
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
    }
}
