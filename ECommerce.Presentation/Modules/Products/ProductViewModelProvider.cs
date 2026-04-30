using AutoMapper;
using ECommerce.Business.Exceptions;
using ECommerce.Business.Modules.Products;
using ECommerce.Domain.Entities;
using ECommerce.Presentation.Modules.Products.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Products
{
    public class ProductViewModelProvider : IProductViewModelProvider
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductViewModelProvider(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        public async Task AddAsync(ProductCreateVM productVM)
        {
            var product = _mapper.Map<Product>(productVM);
            await _productService.AddAsync(product);            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) {
                return false;
            }
            return await _productService.DeleteAsync(product);
        }

        public async Task<IReadOnlyList<ProductListVM>> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            var productList = _mapper.Map<IReadOnlyList<ProductListVM>>(products);
            return productList;
        }

        public async Task<ProductEditVM> GetByIdAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) {
                return null;
            }
            return _mapper.Map<ProductEditVM>(product);
        }

        public async Task UpdateAsync(ProductEditVM productVM)
        {
            var product = _mapper.Map<Product>(productVM);
            await _productService.UpdateAsync(product);
        }
    }
}
