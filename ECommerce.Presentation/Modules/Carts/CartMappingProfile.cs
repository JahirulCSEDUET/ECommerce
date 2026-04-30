using AutoMapper;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Modules.Carts
{
    public class CartMappingProfile:Profile
    {
        public CartMappingProfile()
        {
            CreateMap<Cart, CartItem>();
            CreateMap<CartItem,Cart>();
        }
    }
}
