using ECommerce.DataAccess.Data;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Modules.Categories
{
    public class CategoryRepository:ICategoryRepository
    {

        private readonly ECommerceDbContext _context;

        public CategoryRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<bool> DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludedId=null)
        {
            if (string.IsNullOrWhiteSpace(name)) {
                return false;
            }
            var query = _context.Categories.Where(c=> c.Name.Trim().ToLower() == name.Trim().ToLower());
            if (excludedId.HasValue) 
            { 
                query =query.Where(c=> c.Id != excludedId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
