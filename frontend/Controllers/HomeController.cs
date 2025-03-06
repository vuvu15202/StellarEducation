using Microsoft.AspNetCore.Mvc;
using ASPNET_MVC.Models.Entity;

namespace ASPNET_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();  
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult New()
        {
            return View();
        }
    }
}
