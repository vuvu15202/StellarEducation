using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Exceptions;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Shared.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Shared.Utils;
using ASPNET_API.Application.Services.Interfa;

namespace ASPNET_API.Application.Services
{
    internal class AuthenticateService: IAuthenticateService
    {
        private JwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public AuthenticateService(JwtUtils jwtUtils, AppSettings appSettings, UserService userService)
        {
            _jwtUtils = jwtUtils;
            _appSettings = appSettings;
            _userService = userService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _userService.GetByUsernameAsync(model.UserName);

            // validate
            if (user == null)
                throw new AppException("Tài khỏan không tồn tại.");
            if (!BCryptNet.Verify(model.Password, user.Password))
                throw new AppException("Mật khẩu sai.");
            if (!user.Active)
                throw new AppException("Tài khỏan bị vô hiệu hóa.");

            // authentication successful so generate jwt token
            var jwtToken =  _jwtUtils.GenerateJwtToken(user);
            var userRoles = (await _roleService.GetRolesOfUserAsync(user.UserId))?.ToList();

            return new AuthenticateResponse(user, userRoles, jwtToken);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public UserDTO toDTO(User user, List<Role> roles)
        {
            var userDTO = new UserDTO();
            userDTO.UserId = user.UserId;
            userDTO.UserName = user.UserName;
            userDTO.FirstName = user.FirstName;
            userDTO.LastName = user.LastName;

            List<RoleEnum> rolesEnum = new List<RoleEnum>();
            foreach (Role role in roles)
            {
                switch (role.RoleId)
                {
                    case 1: rolesEnum.Add(RoleEnum.Anonymous); break;
                    case 2: rolesEnum.Add(RoleEnum.Admin); break;
                    case 3: rolesEnum.Add(RoleEnum.Lecturer); break;
                    case 4: rolesEnum.Add(RoleEnum.Student); break;
                    case 5: rolesEnum.Add(RoleEnum.Staff); break;
                    default: rolesEnum.Add(RoleEnum.Anonymous); break;
                }
            }
            userDTO.Roles = rolesEnum;
            return userDTO;
        }

    
    }
}
