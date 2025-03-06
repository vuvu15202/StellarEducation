using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNET_API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_API.Application.Services.Interfa
{
    public interface ILessonService
    {

        Task<IEnumerable<Lesson>> GetAllLessons();
        Task<Lesson> GetLessonById(int id);
        Task<bool> LessonExists(int id);
        Task AddLesson(Lesson lesson);
        Task UpdateLesson(Lesson lesson);
        Task DeleteLesson(int id);
    }
}

