using ASPNET_API.Application.Services;
using ASPNET_API.Application.Services.Interfa;
using ASPNET_API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            var lessons = await _lessonService.GetAllLessons();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var lesson = await _lessonService.GetLessonById(id);
            if (lesson == null) return NotFound();

            return Ok(lesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, Lesson lesson)
        {
            if (id != lesson.LessonId) return BadRequest();

            await _lessonService.UpdateLesson(lesson);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            await _lessonService.AddLesson(lesson);
            return CreatedAtAction(nameof(GetLesson), new { id = lesson.LessonId }, lesson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            await _lessonService.DeleteLesson(id);
            return NoContent();
        }
    }
}

