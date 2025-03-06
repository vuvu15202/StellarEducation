using Microsoft.AspNetCore.Mvc;
using ASPNET_MVC.Models.Entity;
using ASPNET_MVC.Middlewares;
using ASPNET_MVC.Models;

namespace ASPNET_MVC.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        [Authorize(RoleEnum.Student)]
        public IActionResult Enroll()
        {
			return View();
        }

        [Authorize(RoleEnum.Student)]
        public IActionResult Examination()
        {
            return View();
        }
    }
}
