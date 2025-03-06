using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public CategoryRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Where(c => !c.IsDelete).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId && !c.IsDelete);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await GetByIdAsync(categoryId);
            if (category != null)
            {
                category.IsDelete = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
