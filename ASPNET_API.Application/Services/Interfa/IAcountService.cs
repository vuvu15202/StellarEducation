using ASPNET_API.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface IAccountService
    {
        Task<object> GetUserProfile(int userId);
        Task<bool> UpdateProfile(int userId, UserProfileDTO model);
        Task<bool> ChangePassword(int userId, ChangePasswordDTO model);
    }
}
