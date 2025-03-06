using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Models.DTO;
using ASPNET_API.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Services;
using ASPNET_API.Application.Services.Interfa;

namespace ASPNET_API.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public AccountService(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<object> GetUserProfile(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return null;

            return new
            {
                Phonenumber = user.Phone,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Image = user.Image,
                Description = user.Description
            };
        }

        public async Task<bool> UpdateProfile(int userId, UserProfileDTO model)
        {
            var userToUpdate = await _userService.GetByIdAsync(userId);
            if (userToUpdate == null) return false;

            string fileName = userToUpdate.Image;
            if (model.Image != null)
            {
                var uploads = Path.Combine(_configuration["URL:BackendURL"], "assetweb", "lecturer");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                fileName = model.Image.FileName;
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            userToUpdate.FirstName = model.FirstName;
            userToUpdate.LastName = model.LastName;
            userToUpdate.Email = model.Email;
            userToUpdate.Phone = model.PhoneNumber;
            userToUpdate.Address = model.Address;
            userToUpdate.Image = _configuration["URL:BackendURL"] + "/assetweb/lecturer/" + fileName;
            userToUpdate.Description = model.Description;

            await _userService.UpdateAsync(userToUpdate);

            return true;
        }

        public async Task<bool> ChangePassword(int userId, ChangePasswordDTO model)
        {
            var userToUpdate = await _userService.GetByIdAsync(userId);
            if (userToUpdate == null) return false;

            if (!VerifyPassword(userToUpdate.Password, model.OldPassword))
                return false;

            if (model.NewPassword != model.ConfirmPassword)
                return false;

            userToUpdate.Password = HashPassword(model.NewPassword);
            await _userService.UpdateAsync(userToUpdate);


            return true;
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

