using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Infrastructure.Data;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Application.Services.Interfa;

namespace ASPNET_API.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly UserRoleService _userRoleService;
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public AdminService(DonationWebApp_v2Context context, UserRoleService userRoleService, UserService userService, RoleService roleService)
        {
            _context = context;
            _userRoleService = userRoleService;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<List<UserListDTO>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            return users.Select(user => new UserListDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Active = user.Active,
                Role = user.UserRoles.Where(ur => ur.UserId == user.UserId).Select(
                    ur => new RoleDTO
                    {
                        RoleId = ur.RoleId,
                        RoleName = ur.Role.RoleName
                    }
                ).FirstOrDefault(),
            }).ToList();
        }

        public async Task<List<RoleDTO>> GetAllRoles()
        {
            var roles = await _roleService.GetAllAsync();

            return roles.Select(r => new RoleDTO
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
            }).ToList();
        }

        public async Task<bool> UpdateUser(UserEditDTO userEditDTO)
        {
            var user = await _userService.GetByIdAsync(userEditDTO.UserId);
            if (user == null)
            {
                return false;
            }

            user.FirstName = userEditDTO.FirstName;
            user.LastName = userEditDTO.LastName;
            user.Email = userEditDTO.Email;
            user.Phone = userEditDTO.Phone;
            user.Address = userEditDTO.Address;
            user.Active = userEditDTO.Active;

            var userRole = (await _userRoleService.GetUserRolesByUserIdAsync(userEditDTO.UserId)).FirstOrDefault();
            if (userRole == null)
            {
                return false;
            }

            userRole.RoleId = userEditDTO.RoleId;

            //_context.Users.Update(user);
            //_context.UserRoles.Update(userRole);
            //await _context.SaveChangesAsync();
            await _userService.UpdateAsync(user);
            await _userRoleService.UpdateUserRoleAsync(userRole);

            return true;
        }

        public async Task<bool> CreateUser(CreateUserDTO createUserDTO)
        {
            var user = new User
            {
                UserName = createUserDTO.UserName,
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password),
                Email = createUserDTO.Email,
                Phone = createUserDTO.Phone,
                Address = createUserDTO.Address,
                Active = createUserDTO.Active,
                EnrollDate = DateTime.Now
            };

            await _userService.AddAsync(user);

            var userRole = new UserRole
            {
                UserId = user.UserId,
                RoleId = createUserDTO.RoleId
            };

            await _userRoleService.AddUserRoleAsync(userRole);

            return true;
        }
    }
}

