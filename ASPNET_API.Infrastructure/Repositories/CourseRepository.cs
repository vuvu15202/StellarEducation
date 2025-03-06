using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public CourseRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Lecturer)
                .Include(c => c.Lessons)
                .Include(c => c.Reviews)
                .Include(c => c.CourseEnrolls)
                .Where(c => !c.IsDelete)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Lecturer)
                .Include(c => c.Lessons)
                .Include(c => c.Reviews)
                .Include(c => c.CourseEnrolls)
                .FirstOrDefaultAsync(c => c.CourseId == courseId && !c.IsDelete);
        }

        public async Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Courses
                .Where(c => c.CategoryId == categoryId && !c.IsDelete)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByLecturerIdAsync(int lecturerId)
        {
            return await _context.Courses
                .Where(c => c.LecturerId == lecturerId && !c.IsDelete)
                .ToListAsync();
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int courseId)
        {
            var course = await GetByIdAsync(courseId);
            if (course != null)
            {
                course.IsDelete = true;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<User> GetSessionUserAsync(HttpContext httpContext)
        {
            return (User)httpContext.Items["User"];
        }

        public async Task<bool> IsUserEnrolledInLessonAsync(int userId, int lessonId)
        {
            return await _context.CourseEnrolls
                .Include(c => c.Course)
                .ThenInclude(c => c.Lessons)
                .AnyAsync(c => c.UserId == userId && c.Course.Lessons.Any(l => l.LessonId == lessonId));
        }

        public async Task<Lesson> GetLessonWithQuestionBankAsync(int lessonId)
        {
            return await _context.Lessons
                .Include(l => l.QuestionBank)
                .Where(l => l.LessonId == lessonId)
                .SingleOrDefaultAsync();
        }
    }
}
