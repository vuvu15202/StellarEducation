using ASPNET_API.Application.DTOs;
using ASPNET_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface IAuthenticateService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>>  GetAll();
        Task<User> GetById(int id);

        UserDTO toDTO(User user, List<Role> roles);
    }
}
