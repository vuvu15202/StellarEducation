using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNET_API.Application.DTOs;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface IAdminService
    {
        Task<List<UserListDTO>> GetAllUsers();
        Task<List<RoleDTO>> GetAllRoles();
        Task<bool> UpdateUser(UserEditDTO userEditDTO);
        Task<bool> CreateUser(CreateUserDTO createUserDTO);
    }
}
