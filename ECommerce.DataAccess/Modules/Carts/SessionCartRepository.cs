using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.DataAccess.Modules.Carts
{
    public class SessionCartRepository : ICartRepository
    {
        private const string CartSessionKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;
        public SessionCartRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart GetCarts()
        {
            if(Session.TryGetValue(CartSessionKey, out byte[]? bytes ) && bytes != null && bytes.Length>0)
            {
                var json= Encoding.UTF8.GetString(bytes);
                var cart = JsonSerializer.Deserialize<Cart>(json);
                return cart;
            }
            return new Cart();                                                                       
        }

        public void SaveCart(Cart cart) // session support int, string, byte[]
        {
            var json = JsonSerializer.Serialize(cart);
            var encoded = Encoding.UTF8.GetBytes(json);
            Session.Set(CartSessionKey, encoded);
        }

        public void Clear()
        {
            Session.Remove(CartSessionKey);
        }
    }
}
