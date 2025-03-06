using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ASPNET_API.Domain.Repositories
{
    public class CourseEnrollRepository : ICourseEnrollRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public CourseEnrollRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<List<CourseEnroll>> GetAllAsync()
        {
            return await _context.CourseEnrolls.Include(c => c.User).Include(c => c.Course).ToListAsync();
        }

        public async Task<CourseEnroll> GetByCourseIdAsync(int? courseId, int userId)
        {
            return await _context.CourseEnrolls
                .Include(c => c.User)
                .Include(c => c.Course)
                .ThenInclude(ce => ce.Lessons)
                .FirstOrDefaultAsync(ce => ce.CourseId == courseId && ce.UserId == userId);
        }

        public async Task<CourseEnroll> GetByUserIdAsync(int? courseId, int userId)
        {
            return await _context.CourseEnrolls
                .Include(c => c.User)
                .Include(c => c.Course)
                .ThenInclude(ce => ce.Lessons)
                .FirstOrDefaultAsync(ce => ce.UserId == userId);
        }

        public async Task<CourseEnroll> GetByIdAsync(int id)
        {
            return await _context.CourseEnrolls.FindAsync(id);
        }

        public async Task<List<CourseEnroll>> GetByCourseIdsAsync(int? courseId)
        {
            return await _context.CourseEnrolls.Include(c => c.User)
                .Where(ce => ce.CourseId == courseId).ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CourseEnrolls.AnyAsync(e => e.CourseEnrollId == id);
        }

        public async Task AddAsync(CourseEnroll courseEnroll)
        {
            await _context.CourseEnrolls.AddAsync(courseEnroll);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseEnroll courseEnroll)
        {
            _context.CourseEnrolls.Update(courseEnroll);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var courseEnroll = await _context.CourseEnrolls.FindAsync(id);
            if (courseEnroll != null)
            {
                _context.CourseEnrolls.Remove(courseEnroll);
                await _context.SaveChangesAsync();
            }
        }
    }
}
