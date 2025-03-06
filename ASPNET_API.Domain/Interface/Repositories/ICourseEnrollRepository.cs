using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNET_API.Domain.Entities;

namespace ASPNET_API.Domain.Interface.Repositories
{
    public interface ICourseEnrollRepository
    {
        Task<List<CourseEnroll>> GetAllAsync();
        Task<CourseEnroll> GetByCourseIdAsync(int? courseId, int userId);
        Task<CourseEnroll> GetByUserIdAsync(int? courseId, int userId);
        Task<CourseEnroll> GetByIdAsync(int id);
        Task<List<CourseEnroll>> GetByCourseIdsAsync(int? courseId);
        Task<bool> ExistsAsync(int id);
        Task AddAsync(CourseEnroll courseEnroll);
        Task UpdateAsync(CourseEnroll courseEnroll);
        Task DeleteAsync(int id);
    }
}
