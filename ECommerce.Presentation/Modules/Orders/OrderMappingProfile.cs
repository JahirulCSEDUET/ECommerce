using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Presentation.Modules.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Orders
{
    public class OrderMappingProfile:Profile
    {
        public OrderMappingProfile() 
        {
            //Entity --> ViewModel
            CreateMap<OrderItem, OrderItemListVM>()
                // Assuming your OrderItem has a Product navigation property
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<Order, OrderListVM>();
        }
    }
}
