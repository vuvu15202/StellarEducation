using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ASPNET_MVC.Models;
using ASPNET_MVC.Models.Entity;
using ASPNET_MVC.temp;
using ASPNET_MVC.Middlewares;
using System.Net;

namespace ASPNET_MVC.Controllers
{
    [Authorize]
    public class CoursesController : Controller
	{

        private readonly IConfiguration _configuration;

        public CoursesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize(RoleEnum.Student)]
        public async Task<IActionResult> lesson(int? courseId, int? lessonNum = 1)
		{
            HttpClient _httpClient = new HttpClient();
            var apiUrl = _configuration["ApiSettings:BaseAPIUrl"];

            if (courseId == null)
            {
                return Redirect("/Error/Error404");
            }
            var userLoggedIn = HttpContext.Items["UserLoggedIn"] as UserLoggedIn;
            var token = userLoggedIn.AccessToken;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            HttpResponseMessage responseCourse =await _httpClient.GetAsync($"{apiUrl}/Courses/ViewLesson?courseId={courseId}&lessonNum={lessonNum}");
            if (responseCourse.StatusCode != HttpStatusCode.OK)
            {
                return Redirect("/Error/Error404");
            }
            var redirectURL = responseCourse.Content.ReadAsStringAsync().Result;

            if (!"none".Equals(redirectURL))
            {
                return Redirect(redirectURL);
            }
            return View();
		}

        [Authorize(RoleEnum.Student)]
        public IActionResult Payment()
        {
            return View();
        }

        [HttpGet("Courses")]
        [HttpGet("Courses/index")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet("Courses/Detail")]
        public IActionResult CourseDetail(int id)
        {
            return View("CourseDetail");
        }
    }
    
}


//HttpClient _httpClient = new HttpClient();

//if (courseId == null)
//{
//    return Redirect("/Error/Error404");
//}
//HttpResponseMessage responseCourse = _httpClient.GetAsync($"https://localhost:4000/api/Courses/{courseId}").Result;
//if (responseCourse.StatusCode != HttpStatusCode.OK)
//{
//    return Redirect($"/Courses/payment?courseId={courseId}&statuscode=4");
//}
//var courseInfo = responseCourse.Content.ReadFromJsonAsync<Course>().Result;
//if (courseInfo == null)
//{
//}
