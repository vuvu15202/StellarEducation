using ASPNET_API.Application.Services.Interfa;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(int status, string message)> SaveImageAsync(IFormFile imageFile)
        {
            try
            {
                var contentPath = _configuration["URL:BackendURL"];
                // path = "c://projects/productminiapi/wwwroot/images" ,not exactly something like that
                var path = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);

                var stream = new FileStream(fileWithPath, FileMode.Create);
                await imageFile.CopyToAsync(stream);
                stream.Close();
                return new(1, _configuration["URL:BackendURL"] + "/uploads/" + newFileName);
            }
            catch (Exception ex)
            {
                return new(0, "Error has occured");
            }
        }

        public async Task DeleteImageAsync(string imageFileName)
        {
            var contentPath = _configuration["URL:BackendURL"];
            var path = Path.Combine(contentPath, "wwwroot", "uploads", imageFileName);
            if (File.Exists(path))
                File.Delete(path);
        }

        public Task<(int status, string message)> ImportQzuiz(IFormFile QuizFile)
        {
            throw new NotImplementedException();
        }
    }

}
