using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPNET_MVC.Models;
using ASPNET_MVC.Models.Entity;
using ASPNET_MVC.Middlewares;

namespace ASPNET_MVC.Controllers.Admin
{
    [Authorize]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        // GET: AdminController
        [HttpGet("billings")]
        public ActionResult Billing()
        {
            return View();
        }


        [HttpGet("projects")]
        [Authorize(RoleEnum.Admin)]
        public ActionResult ListProjectAdmin()
        {
            return View("Project");
        }



        [HttpGet("dashboard")]
        [Authorize(RoleEnum.Admin)]
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        [HttpGet("studentfee")]
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        public ActionResult StudentFee()
        {
            return View("StudentFee");
        }

        [HttpGet("Notification")]
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        public ActionResult Notification()
        {
            return View("Notification");
        }


        [Authorize(RoleEnum.Lecturer, RoleEnum.Admin)]
        [HttpGet("Courses")]
        public ActionResult Courses()
        {
            return View("Courses");
        }
        [Authorize(RoleEnum.Lecturer, RoleEnum.Admin)]
        [HttpGet("Coursess")]
        public ActionResult Coursess()
        {
            return View("Coursess");
        }

        [Authorize(RoleEnum.Lecturer, RoleEnum.Admin)]
        [HttpGet("Lessons")]
        public ActionResult Lessons()
        {
            return View("Lessons");
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Lecturer)]
        [HttpGet("Categories")]
        public ActionResult Categories()
        {
            return View();
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        [HttpGet("usermanagement")]
        public IActionResult UserManagement()
        {
            return View("UserManagement");
        }


        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        [HttpGet("questionBanks")]
        public IActionResult questionBank()
        {
            return View("questionBanks");
        }

		[Authorize(RoleEnum.Admin, RoleEnum.Staff)]
		[HttpGet("consultationrequests")]
		public IActionResult consultationRequest()
		{
			return View("ConsultationRequests");
		}



		[Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        [HttpGet("QuestionBankEdit/Index")]
        public IActionResult QuestionBanksEditIndex()
        {
            return View("QuestionBankEdit/Index");
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        [HttpGet("QuestionBankEdit/Reading")]
        public IActionResult QuestionBanksEditReading()
        {
            return View("QuestionBankEdit/Reading");
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        [HttpGet("QuestionBankEdit/Listening")]
        public IActionResult QuestionBanksEditListening()
        {
            return View("QuestionBankEdit/Listening");
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        [HttpGet("QuestionBankEdit/Writing")]
        public IActionResult QuestionBanksEditWriting()
        {
            return View("QuestionBankEdit/Writing");
        }

        //// GET: AdminController/Details/5
        //[HttpGet]
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AdminController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


    }
}
