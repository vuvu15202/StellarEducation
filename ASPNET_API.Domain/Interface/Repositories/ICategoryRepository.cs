using ASPNET_API.Domain.Entities;

namespace ASPNET_API.Domain.Interface.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int categoryId);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int categoryId);
    }
}
