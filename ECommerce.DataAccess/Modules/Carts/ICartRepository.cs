using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.Modules.Carts
{
    public interface ICartRepository
    {
        Cart GetCarts();
        void SaveCart(Cart cart);
        void Clear();
    }
}
