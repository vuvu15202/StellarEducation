using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNET_API.Application.Services.Interfa;
using ASPNET_API.Application.DTOs;

namespace ASPNET_API.Domain.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<Lesson>> GetAllLessons()
        {
            return await _lessonRepository.GetLessons();
        }

        public async Task<Lesson> GetLessonById(int id)
        {
            return await _lessonRepository.GetLesson(id);
        }

        public async Task<bool> LessonExists(int id)
        {
            return await _lessonRepository.LessonExists(id);
        }

        public async Task AddLesson(Lesson lesson)
        {
            await _lessonRepository.AddLesson(lesson);
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            await _lessonRepository.UpdateLesson(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            var lesson = await _lessonRepository.GetLesson(id);
            if (lesson != null)
            {
                await _lessonRepository.DeleteLesson(lesson);
            }
        }

        public async Task<Lesson> CreateLessonAsync(LessonModel lessonModel)
        {
            QuestionBank questionBank = null;
            if (!string.IsNullOrEmpty(lessonModel.Quiz))
            {
                questionBank = await _lessonRepository.GetQuestionBankByExamCodeAsync(lessonModel.Quiz);
                if (questionBank == null) throw new Exception("Không tìm thấy exam code!");
            }

            var existingLesson = await _lessonRepository.GetLessonByLessonNumAsync(lessonModel.LessonNum, lessonModel.CourseId);
            if (existingLesson != null) throw new Exception("Đã có bài giảng mang số thứ tự này");

            var lesson = new Lesson
            {
                LessonNum = lessonModel.LessonNum,
                CourseId = lessonModel.CourseId,
                Name = lessonModel.Name,
                Description = lessonModel.Description,
                VideoUrl = lessonModel.VideoUrl,
                VideoTime = lessonModel.VideoTime,
                Quiz = lessonModel.Quiz,
                PreviousLessioNum = lessonModel.PreviousLessioNum,
                QuestionBankId = questionBank?.QuestionBankId
            };

            return await _lessonRepository.AddLesson(lesson);
        }

        public async Task<Lesson> UpdateLessonAsync(int id, LessonModel lessonModel)
        {
            var lesson = await _lessonRepository.GetLesson(id);
            if (lesson == null) throw new Exception("Không tìm thấy bài giảng!");

            QuestionBank questionBank = null;
            if (!string.IsNullOrEmpty(lessonModel.Quiz))
            {
                questionBank = await _lessonRepository.GetQuestionBankByExamCodeAsync(lessonModel.Quiz);
                if (questionBank == null) throw new Exception("Không tìm thấy exam code!");
            }

            var existingLesson = await _lessonRepository.GetLessonByLessonNumAsync(lessonModel.LessonNum, lessonModel.CourseId, id);
            if (existingLesson != null) throw new Exception("Đã có bài giảng mang số thứ tự này");

            lesson.LessonNum = lessonModel.LessonNum;
            lesson.CourseId = lessonModel.CourseId;
            lesson.Name = lessonModel.Name;
            lesson.Description = lessonModel.Description;
            lesson.VideoUrl = lessonModel.VideoUrl;
            lesson.VideoTime = lessonModel.VideoTime;
            lesson.Quiz = lessonModel.Quiz;
            lesson.PreviousLessioNum = lessonModel.PreviousLessioNum;
            lesson.IsDelete = lessonModel.IsDelete;
            lesson.QuestionBankId = questionBank?.QuestionBankId;

            var updatedLesson = await _lessonRepository.UpdateLesson(lesson);

            if (lesson.Course != null)
            {
                lesson.Course.UpdatedAt = DateTime.Now;
                await _lessonRepository.UpdateLesson(lesson);
            }

            return updatedLesson;
        }
    }
}
