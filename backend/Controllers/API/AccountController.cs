using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Authorization;
using ASPNET_API.Domain.Entities;
namespace ASPNET_API.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer, RoleEnum.Student)]
		[HttpGet]
        public IActionResult GetUserProfile()
        {
            var u = (User)HttpContext.Items["User"];
            var user = new
            {
                Phonenumber = u.Phone,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                Image = u.Image,
                Description = u.Description
            };

            return Ok(user);
        }

		[Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer, RoleEnum.Student)]
		[HttpPut]
        public async Task<IActionResult> UpdateProfile([FromForm] UserProfileDTO model)
        {
            var user = (User)HttpContext.Items["User"];

            try
            {
                var userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (userToUpdate == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                string fileName = "";
                if (model.Image != null)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "assetweb", "lecturer");

                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var filePath = Path.Combine(uploads, model.Image.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                        fileName = model.Image.FileName;
                    }
                }

                // Cập nhật thông tin từ model vào userToUpdate
                userToUpdate.FirstName = model.FirstName;
                userToUpdate.LastName = model.LastName;
                userToUpdate.Email = model.Email;
                userToUpdate.Phone = model.PhoneNumber;
                userToUpdate.Address = model.Address;
                userToUpdate.Image = String.IsNullOrEmpty(fileName) ? userToUpdate.Image : _configuration["URL:BackendURL"] + "/assetweb/lecturer/" + fileName;
                userToUpdate.Description = model.Description;

                _context.Update(userToUpdate);  

                _context.SaveChanges();

                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
		[Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer, RoleEnum.Student)]
		[HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var user = (User)HttpContext.Items["User"];

            try
            {
                var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
                if (userToUpdate == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                if (!VerifyPassword(userToUpdate.Password, model.OldPassword))
                {
                    return BadRequest(new { message = "Old password is incorrect" });
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    return BadRequest(new { message = "New password and confirm password do not match" });
                }

                userToUpdate.Password = HashPassword(model.NewPassword);

                _context.Users.Update(userToUpdate);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Password updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        private bool VerifyPassword(string savedPasswordHash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, savedPasswordHash);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);

        }
    }
}