using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.Modules.Products
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
        Task<Product> AddAsync(Product product);
        Task<bool> ExistsBySKUAsync(string sku, int? excludedId = null);
    }
}
