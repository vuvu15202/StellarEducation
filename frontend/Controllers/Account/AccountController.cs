using Microsoft.AspNetCore.Mvc;
using ASPNET_MVC.Models;
using ASPNET_MVC.Models.Entity;
using ASPNET_MVC.Middlewares;

namespace ASPNET_MVC.Controllers.Account
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer, RoleEnum.Student)]
        [HttpGet("UserProfile")]
        public IActionResult UserProfile()
        {
            return View("UserProfile");
        }
    }
}
