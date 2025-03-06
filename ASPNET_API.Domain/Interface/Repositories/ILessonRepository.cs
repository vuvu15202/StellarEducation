using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNET_API.Domain.Entities;

namespace ASPNET_API.Domain.Interface.Repositories
{
    public interface ILessonRepository
    {
        Task<IEnumerable<Lesson>> GetLessons();
        Task<Lesson> GetLesson(int id);
        Task<bool> LessonExists(int id);
        Task<Lesson> AddLesson(Lesson lesson);
        Task<Lesson> UpdateLesson(Lesson lesson);
        Task DeleteLesson(Lesson lesson);
        Task<Lesson> GetLessonByLessonNumAsync(int lessonNum, int courseId, int? excludeLessonId = null);
        Task<QuestionBank> GetQuestionBankByExamCodeAsync(string examCode);
    }
}

