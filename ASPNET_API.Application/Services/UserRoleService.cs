using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNET_API.Application.Services.Interfa;

namespace ASPNET_API.Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public async Task<UserRole?> GetUserRoleByIdAsync(int id)
        {
            return await _userRoleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesByUserIdAsync(int userId)
        {
            return await _userRoleRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesByRoleIdAsync(int roleId)
        {
            return await _userRoleRepository.GetByRoleIdAsync(roleId);
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _userRoleRepository.AddAsync(userRole);
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            await _userRoleRepository.UpdateAsync(userRole);
        }

        public async Task DeleteUserRoleAsync(int id)
        {
            await _userRoleRepository.DeleteAsync(id);
        }
    }
}
