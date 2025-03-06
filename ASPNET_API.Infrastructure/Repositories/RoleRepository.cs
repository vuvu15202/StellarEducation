using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public RoleRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .Where(r => !r.RoleName.ToUpper().Equals("ANONYMOUS") && !r.RoleName.ToUpper().Equals("ADMIN"))
                .ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.RoleId == id);
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await GetByIdAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Role>> GetRolesOfUserAsync(int userId)
        {
            return await _context.Roles.Where(r => r.UserRoles.Any(ur => ur.UserId == userId)).ToListAsync();
        }
    }
}
