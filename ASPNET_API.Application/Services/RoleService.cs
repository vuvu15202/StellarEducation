using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;

namespace ASPNET_API.Application.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesOfUserAsync(int userId)
        {
            return await _repository.GetRolesOfUserAsync(userId);
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _repository.GetByNameAsync(roleName);
        }

        public async Task AddAsync(Role role)
        {
            await _repository.AddAsync(role);
        }

        public async Task UpdateAsync(Role role)
        {
            await _repository.UpdateAsync(role);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

