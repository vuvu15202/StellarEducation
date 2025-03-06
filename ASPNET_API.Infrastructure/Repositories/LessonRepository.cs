using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly DonationWebApp_v2Context _context;

        public LessonRepository(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> GetLessons()
        {
            return await _context.Lessons.ToListAsync();
        }

        public async Task<Lesson> GetLesson(int id)
        {
            return await _context.Lessons.FindAsync(id);
        }

        public async Task<bool> LessonExists(int id)
        {
            return await _context.Lessons.IgnoreQueryFilters().AnyAsync(e => e.LessonId == id);
        }

        public async Task AddLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLesson(Lesson lesson)
        {
            lesson.IsDelete = true;
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> GetLessonByIdAsync(int id)
        {
            return await _context.Lessons.Include(l => l.Course).SingleOrDefaultAsync(l => l.LessonId == id);
        }

        public async Task<Lesson> GetLessonByLessonNumAsync(int lessonNum, int courseId, int? excludeLessonId = null)
        {
            return await _context.Lessons
                .Where(l => l.LessonNum == lessonNum && l.IsDelete == false && l.CourseId == courseId && (excludeLessonId == null || l.LessonId != excludeLessonId))
                .FirstOrDefaultAsync();
        }

        public async Task<QuestionBank> GetQuestionBankByExamCodeAsync(string examCode)
        {
            return await _context.QuestionBanks
                .Where(q => q.ExamCode.ToLower().Equals(examCode.ToLower()))
                .SingleOrDefaultAsync();
        }
    }
}
