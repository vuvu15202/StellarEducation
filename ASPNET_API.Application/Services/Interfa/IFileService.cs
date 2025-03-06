using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface IFileService
    {
        Task<(int status, string message)> SaveImageAsync(IFormFile imageFile);
        Task DeleteImageAsync(string imageFileName);

        Task<(int status, string message)> ImportQzuiz(IFormFile QuizFile);
    }
}
