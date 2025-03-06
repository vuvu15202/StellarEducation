using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ASPNET_API.Models.Entity;
using ASPNET_API.temp;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;

        public LessonsController(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
          if (_context.Lessons == null)
          {
              return NotFound();
          }
            return await _context.Lessons.ToListAsync();
        }


        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
          if (_context.Lessons == null)
          {
              return NotFound();
          }
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutLesson/{id}")]
        public async Task<IActionResult> PutLesson(int id, Lesson lesson)
        {
            if (id != lesson.LessonId)
            {
                return BadRequest();
            }

           _context.Entry(lesson).State = EntityState.Modified;

           try
           {
               await _context.SaveChangesAsync();
           }
           catch (DbUpdateConcurrencyException)
           {
               if (!LessonExists(id))
               {
                   return NotFound();
               }
               else
               {
                   throw;
               }
           }

           return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        //{
        //  if (_context.Lessons == null)
        //  {
        //      return Problem("Entity set 'DonationWebApp_v2Context.Lessons'  is null.");
        //  }
        //    _context.Lessons.Add(lesson);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLesson", new { id = lesson.LessonId }, lesson);
        //}

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            if (_context.Lessons == null)
            {
                return NotFound();
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            lesson.IsDelete = true;
            _context.Lessons.Update(lesson);
            //_context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonExists(int id)
        {
            return (_context.Lessons?.IgnoreQueryFilters().Any(e => e.LessonId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Postt([FromForm] LessonModel lesson)
        {

            QuestionBank questionBank = null;
            if (lesson.Quiz != null)
            {
                questionBank = await _context.QuestionBanks.Where(q => q.ExamCode.ToLower().Equals(lesson.Quiz.ToLower())).SingleOrDefaultAsync();
                if (questionBank == null) return NotFound("Không tìm thấy exam code!");
            }
            var checkLessonNum = await _context.Lessons.Where(l => l.LessonNum == lesson.LessonNum && l.IsDelete == false && l.CourseId == lesson.CourseId).FirstOrDefaultAsync();
            if (checkLessonNum != null) return BadRequest("Đã có bài giảng mang số thứ tự này");

            var les = new Lesson()
            {
                LessonNum = lesson.LessonNum,
                CourseId = lesson.CourseId,
                Name = lesson.Name,
                Description = lesson.Description,
                VideoUrl = lesson.VideoUrl,
                VideoTime = lesson.VideoTime,
                Quiz = lesson.Quiz,
                PreviousLessioNum = lesson.PreviousLessioNum,
                QuestionBankId = questionBank == null ? null : questionBank.QuestionBankId,
            };
            _context.Lessons.Add(les);
            await _context.SaveChangesAsync();

            // Xử lý nội dung JSON từ fileContentJson ở đây
            // Ví dụ: bạn có thể in nội dung của JSON ra console để kiểm tra
            //System.Diagnostics.Debug.WriteLine(fileContentJson);

            return Ok(les);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Puttin(int id, [FromForm] LessonModel lesson)
        {
            if (id != lesson.LessonId)
            {
                return BadRequest();
            }

            var checkLesson = _context.Lessons.IgnoreQueryFilters().AsNoTracking().Include(l => l.Course).SingleOrDefault(l => l.LessonId == id);
            if(checkLesson == null)
            {
                return NotFound("Không tìm thấy bài giảng!");
            }

            
            try
            {
                QuestionBank questionBank = null;
                if (lesson.Quiz != null) {
                    questionBank = await _context.QuestionBanks.Where(q => q.ExamCode.ToLower().Equals(lesson.Quiz.ToLower())).SingleOrDefaultAsync();
                    if (questionBank == null) return NotFound("Không tìm thấy exam code!");
                }
                var checkLessonNum = await _context.Lessons.Where(l => l.LessonNum == lesson.LessonNum && l.IsDelete == false && l.CourseId == lesson.CourseId && l.LessonId != lesson.LessonId).FirstOrDefaultAsync();
                if (checkLessonNum != null) return BadRequest("Đã có bài giảng mang số thứ tự này");

                checkLesson.LessonNum = lesson.LessonNum;
                checkLesson.CourseId = lesson.CourseId;
                checkLesson.Name = lesson.Name;
                checkLesson.Description = lesson.Description;
                checkLesson.VideoUrl = lesson.VideoUrl;
                checkLesson.VideoTime = lesson.VideoTime;
                checkLesson.Quiz = lesson.Quiz;
                checkLesson.PreviousLessioNum = lesson.PreviousLessioNum;
                checkLesson.IsDelete = lesson.IsDelete;
                checkLesson.QuestionBankId = questionBank == null ? null : questionBank.QuestionBankId;


                _context.Lessons.Update(checkLesson);
                await _context.SaveChangesAsync();

                var course = checkLesson.Course;
                if (course != null)
                {
                    course.UpdatedAt = DateTime.Now;
                    _context.Courses.Update(course);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(checkLesson);
        }
    }

    

}
