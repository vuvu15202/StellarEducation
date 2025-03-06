using ASPNET_API.Application.DTOs;
using ASPNET_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASPNET_API.Services
{
    public interface ICourseEnrollService
    {
        Task<List<CourseEnrollDTO>> GetAllCourseEnrollsAsync();
        Task<CourseEnrollDTO> GetCourseEnrollByCourseIdAsync(int? courseId, int userId);
        Task<CourseEnrollDTO> GetCourseEnrollByIdAsync(int id);
        Task<List<CourseEnrollDTO>> GetCourseEnrollsByCourseIdAsync(int? courseId);
        Task<bool> CourseEnrollExistsAsync(int id);
        Task AddCourseEnrollAsync(CourseEnrollDTO courseEnrollDto);
        Task UpdateCourseEnrollAsync(int id, CourseEnrollDTO courseEnrollDto);
        Task DeleteCourseEnrollAsync(int id);
    }
}
