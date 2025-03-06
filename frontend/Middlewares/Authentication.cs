using ASPNET_MVC.Models.Entity;
using System.Net.Http;
using System.Text.Json;

namespace ASPNET_MVC.Middlewares
{

    public class Authentication
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public Authentication(RequestDelegate next, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Sử dụng IHttpContextAccessor để lấy HttpContext
            _httpContextAccessor.HttpContext.Items["UserLoggedIn"] = getTokenCookies();
            await _next(httpContext);
        }

        public UserLoggedIn getTokenCookies()
        {
            HttpClient _httpClient = new HttpClient();
            var apiUrl = _configuration["ApiSettings:BaseAPIUrl"];
            try
            {
                string apiurlToken = string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Cookies["apiurl"]) ? "" : _httpContextAccessor.HttpContext.Request.Cookies["apiurl"];
                if (apiurlToken.Equals(""))
                {
                    setAPIURLCookie(apiUrl);
                }
                string jsonData = string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Cookies["UserInfo"]) ? "" : _httpContextAccessor.HttpContext.Request.Cookies["UserInfo"];
                if (!jsonData.Equals(""))
                {
                    return JsonSerializer.Deserialize<UserLoggedIn>(jsonData);
                }
                else
                {
                    HttpResponseMessage response = _httpClient.GetAsync($"{apiUrl}/auth/AccessTokenAnonymous").Result;
                    return response.Content.ReadFromJsonAsync<UserLoggedIn>().Result;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = _httpClient.GetAsync($"{apiUrl}/auth/AccessTokenAnonymous").Result;
                return response.Content.ReadFromJsonAsync<UserLoggedIn>().Result;
            }

        }

        public void setAPIURLCookie(string? apiurl)
        {
            var cookieOptions1 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1) // Set in the past
            }; _httpContextAccessor.HttpContext.Response.Cookies.Append("apiurl", "", cookieOptions1);


            var cookieOptions2 = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1), // Set the expiry date
                //HttpOnly = true, // A security measure / Loại bỏ HttpOnly nếu cần truy cập từ JavaScript.
                Secure = false // Send the cookie over HTTPS only
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("apiurl", apiurl, cookieOptions2);

        }
    }

}
