using ASPNET_API.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IConfiguration _configuration;

        public LecturersController(DonationWebApp_v2Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> GetLecturers()
        {
            var baseURL = _configuration["URL:BackendURL"] + "/assetweb/lecturer/"; 
			var lecturers = new List<object>();
            //lecturers.Add(new {id=1, Name = "Bao Lam", Image = $"{baseURL}baolam.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem" });
            //lecturers.Add(new { id = 2, Name = "Duy Hung", Image = $"{baseURL}duyhung.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem" });
            //lecturers.Add(new { id = 3, Name = "Hong Minh", Image = $"{baseURL}hongminh.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem" });
            //lecturers.Add(new { id = 4, Name = "Quan Bao", Image = $"{baseURL}quanbao.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem" });
            //lecturers.Add(new { id = 5, Name = "Son Long", Image = $"{baseURL}sonlong.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem" });
            //lecturers.Add(new { id = 6, Name = "Viet Bach", Image = $"{baseURL}vietbach.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem" });
            //lecturers.Add(new { id = 7, Name = "Viet Ha", Image = $"{baseURL}vietha.jpg", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem" });

            return Ok(lecturers);
        }
    }
}
