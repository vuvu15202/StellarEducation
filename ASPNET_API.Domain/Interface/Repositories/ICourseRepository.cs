using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ASPNET_API.Domain.Entities;

namespace ASPNET_API.Domain.Interface.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int courseId);
        Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Course>> GetByLecturerIdAsync(int lecturerId);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int courseId);


        Task<User> GetSessionUserAsync(HttpContext httpContext);
        Task<bool> IsUserEnrolledInLessonAsync(int userId, int lessonId);
        Task<Lesson> GetLessonWithQuestionBankAsync(int lessonId);
    }
}
