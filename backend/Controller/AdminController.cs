using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Services.Interfa;
using ASPNET_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASPNET_API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserEditDTO userEditDTO)
        {
            var success = await _adminService.UpdateUser(userEditDTO);
            if (!success)
            {
                return NotFound();
            }
            return Ok(userEditDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var roles = await _adminService.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            var success = await _adminService.CreateUser(createUserDTO);
            if (!success)
            {
                return BadRequest("User creation failed.");
            }
            return Ok();
        }
    }
}

