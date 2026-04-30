using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Presentation.Modules.Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Products
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile() 
        {
            
            //ViewModel -> Entity
            CreateMap<ProductCreateVM, Product>();
            CreateMap<ProductEditVM, Product>();
            //Entity -> ViewModel
            CreateMap<Product, ProductListVM>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : string.Empty));
            CreateMap<Product, ProductEditVM>();
        }
    }
}
