using ECommerce.DataAccess.Data;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Modules.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> ExistsBySKUAsync(string sku, int? excludedId = null)
        {
            if (string.IsNullOrWhiteSpace(sku)) 
            {
                return false;
            }
            var query = _context.Products.Where(p=> p.SKU.Trim().ToLower()== sku.Trim().ToLower());
            if (excludedId.HasValue) 
            {
                query = query.Where(p=> p.Id!=excludedId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Include(c=> c.Category)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p=> p.Id==id);
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
