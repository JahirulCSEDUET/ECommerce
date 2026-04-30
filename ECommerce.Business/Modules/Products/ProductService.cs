using ECommerce.Business.Exceptions;
using ECommerce.DataAccess.Modules.Products;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Modules.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var existence = await _productRepository.ExistsBySKUAsync(product.SKU);
            if (existence)
            {
                throw new InvalidUserInputExceptions($"A product with this SKU: {product.SKU} is already exit!");
            }
            product.CreatedDate = DateTime.Now;
            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            var existence = await _productRepository.ExistsBySKUAsync(product.SKU, product.Id);
            if (existence)
            {
                throw new InvalidUserInputExceptions($"A product with this SKU: {product.SKU} is already exit!");
            }
            product.UpdateDate = DateTime.Now;
            await _productRepository.UpdateAsync(product);
        }
    }
}
