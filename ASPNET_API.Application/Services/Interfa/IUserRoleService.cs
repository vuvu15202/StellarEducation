using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        Task<UserRole?> GetUserRoleByIdAsync(int id);
        Task<IEnumerable<UserRole>> GetUserRolesByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetUserRolesByRoleIdAsync(int roleId);
        Task AddUserRoleAsync(UserRole userRole);
        Task UpdateUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(int id);
    }
}

