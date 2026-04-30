using ECommerce.DataAccess.Data;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.Modules.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;

        public OrderRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o=> o.OrderItems)
                    
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o=>o.OrderItems)
                .ThenInclude(op=>op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
