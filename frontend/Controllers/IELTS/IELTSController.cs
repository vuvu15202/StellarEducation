using ASPNET_MVC.Middlewares;
using ASPNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_MVC.Controllers.IELTS
{
    [Route("IELTS")]
    public class IELTSController : Controller
    {

		[HttpGet("Trial")]
		public ActionResult Trial()
		{
			return View("Trial");
		}

        //Test
		[Authorize(RoleEnum.Student)]
        [HttpGet("Test")]
        public ActionResult Index()
        {
            return View("Test/Index");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Test/Reading")]
        public ActionResult Reading()
        {
            return View("Test/Reading");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Test/Listening")]
        public ActionResult Listening()
        {
            return View("Test/Listening");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Test/Writing")]
        public ActionResult Writing()
        {
            return View("Test/Writing");
        }


        //Quiz
        [Authorize(RoleEnum.Student)]
        [HttpGet("Quiz")]
        public ActionResult QuizIndex()
        {
            return View("Quiz/Index");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Quiz/Reading")]
        public ActionResult QuizReading()
        {
            return View("Quiz/Reading");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Quiz/Listening")]
        public ActionResult QuizListening()
        {
            return View("Quiz/Listening");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Quiz/Writing")]
        public ActionResult QuizWriting()
        {
            return View("Quiz/Writing");
        }


        //review
        [Authorize(RoleEnum.Student)]
        [HttpGet("Review")]
        public ActionResult ReviewIndex()
        {
            return View("Review/Index");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Review/Reading")]
        public ActionResult ReviewReading()
        {
            return View("Review/Reading");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Review/Listening")]
        public ActionResult ReviewListening()
        {
            return View("Review/Listening");
        }

        [Authorize(RoleEnum.Student)]
        [HttpGet("Review/Writing")]
        public ActionResult ReviewWriting()
        {
            return View("Review/Writing");
        }

    }
}
