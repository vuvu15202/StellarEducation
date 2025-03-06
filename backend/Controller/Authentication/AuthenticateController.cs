using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Services;
using BCryptNet = BCrypt.Net.BCrypt;
using ASPNET_API.Authorization;
using ASPNET_API.Application.Services;
using ASPNET_API.Application.Services.Interfa;


namespace ASPNET_API.Controller.Authentication
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IAuthenticateService _authorizationService;
        private UserService _userService;
        private UserRoleService _userRoleService;
        private IJwtUtils _jwtUtils;

        public AuthenticateController(IAuthenticateService authorizationService, IJwtUtils jwtUtils, UserService userService, UserRoleService userRoleService)
        {
            _authorizationService = authorizationService;
            _userService = userService;
            _jwtUtils = jwtUtils;_userRoleService = userRoleService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = await _authorizationService.Authenticate(model);
                CookieHelper.SetTokenCookies(Response, response.AccessToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> AccessTokenAnonymous()
        {
            var user = await _userService.GetByIdAsync(1);
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var userRoles = (await _userRoleService.GetUserRolesByUserIdAsync(user.UserId)).Select(u => u.RoleId).ToList();
            var response = new UserLoggedIn()
            {
                UserInfo = user,
                AccessToken = jwtToken,
                Roles = userRoles
            };
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticateRequest model)
        {
            var checkUser = await _userService.GetByUsernameAsync(model.UserName.ToLower());
            var checkUserEmail = await _userService.GetByEmailAsync(model.Email);
            if (checkUser != null) return BadRequest("This username have already exist!");
            if (checkUserEmail != null) return BadRequest("This email have already exist!");

            try
            {
                var newUser = new User();
                newUser.UserName = model.UserName;
                newUser.Password = BCryptNet.HashPassword(model.Password);
                newUser.FirstName = "userFirstName";
                newUser.LastName = "userLastName";
                newUser.Phone = "";
                newUser.Address = "";
                newUser.Email = model.Email;
                newUser.EnrollDate = DateTime.Now;

                await _userService.AddAsync(newUser);

                var userRole = new UserRole();
                userRole.RoleId = 4;
                userRole.UserId = newUser.UserId;
                await _userRoleService.AddUserRoleAsync(userRole);

                return Ok(new AuthenticateResponse(newUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

    }
    public class CookieHelper
    {
        public static void SetTokenCookies(HttpResponse response, string? token)
        {
            var cookieOptions1 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            response.Cookies.Append("token", "", cookieOptions1);

            if (!string.IsNullOrEmpty(token))
            {
                var cookieOptions2 = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    Secure = false
                };
                response.Cookies.Append("token", token, cookieOptions2);
            }
        }
    }

}
