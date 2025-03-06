using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> UpdateCategoryAsync(UpdateCategory request);
        Task<Category?> CreateCategoryAsync(CreateCategory request);
        Task<bool> DeleteCategoryAsync(int id);
    }
}

