using ASPNET_API.Entities.DTO;
using ASPNET_MVC.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ASPNET_MVC.Controllers.Auth
{
    public class FormLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: AuthController
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleForm(FormLogin formLogin)
        {
            var apiUrl = _configuration["ApiSettings:BaseAPIUrl"];

            //var formData = new List<KeyValuePair<string, string>>
            //{
            //    new KeyValuePair<string, string>("username", formLogin.Username.ToString()),
            //    new KeyValuePair<string, string>("password", formLogin.Password)
            //};

            //var content = new FormUrlEncodedContent(formData);

            var content = new StringContent(
                JsonSerializer.Serialize(formLogin),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.PostAsync($"{apiUrl}/auth/Authenticate", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.StatusCode = response.StatusCode;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return View("Login");
            }
            var userInfo = response.Content.ReadFromJsonAsync<AuthenticateResponse>().Result;
            setCartCookies(new UserLoggedIn { UserInfo = userInfo.UserInfo, AccessToken = userInfo.AccessToken, Roles = userInfo.Roles.Select(u => u.RoleId).ToList()});
            setTokenCookies(userInfo.AccessToken);
            setUserIdCookies(userInfo.UserInfo.UserId.ToString());
            return Redirect(userInfo.RedirectUrl);
        }

        public void setCartCookies(UserLoggedIn? userInfo)
        {
            var cookieOptions1 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1) // Set in the past
            }; Response.Cookies.Append("UserInfo", "", cookieOptions1);


            var cookieOptions2 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1), // Set the expiry date
                //HttpOnly = true, // A security measure
                Secure = false // Send the cookie over HTTPS only
            };

            JsonSerializerOptions options = new JsonSerializerOptions();
            //format dep
            options.WriteIndented = true;
            string jsonData = JsonSerializer.Serialize(userInfo, options);

            Response.Cookies.Append("UserInfo", jsonData, cookieOptions2);

        }

        public void setTokenCookies(string? token)
        {
            var cookieOptions1 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1) // Set in the past
            }; Response.Cookies.Append("token", "", cookieOptions1);


            var cookieOptions2 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1), // Set the expiry date
                //HttpOnly = true, // A security measure / Loại bỏ HttpOnly nếu cần truy cập từ JavaScript.
                Secure = false // Send the cookie over HTTPS only
            };
            Response.Cookies.Append("token", token, cookieOptions2);

        }

        public void setUserIdCookies(string? token)
        {
            var cookieOptions1 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1) // Set in the past
            }; Response.Cookies.Append("userId", "", cookieOptions1);


            var cookieOptions2 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1), // Set the expiry date
                //HttpOnly = true, // A security measure
                Secure = false // Send the cookie over HTTPS only
            };

            JsonSerializerOptions options = new JsonSerializerOptions();
            //format dep
            options.WriteIndented = true;
            string jsonData = JsonSerializer.Serialize(token, options);

            Response.Cookies.Append("userId", jsonData, cookieOptions2);

        }

        // GET: AuthController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
