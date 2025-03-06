using ASPNET_API.Domain.Entities;

using ASPNET_API.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

using ASPNET_API.Application.Services;
using ASPNET_API.Infrastructure.Data;
using ASPNET_API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNET_API.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseEnrollsController : ControllerBase
    {
        private readonly CourseEnrollService _service;
        private readonly DonationWebApp_v2Context _context;


        public CourseEnrollsController(CourseEnrollService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseEnrollDTO>>> GetCourseEnrolls()
        {
            return Ok(await _service.GetAllCourseEnrollsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseEnrollDTO>> GetCourseEnroll(int id)
        {
            var courseEnroll = await _service.GetCourseEnrollByIdAsync(id);
            if (courseEnroll == null) return NotFound();
            return Ok(courseEnroll);
        }

        [HttpGet("getcourseenroll/{courseId}")]
        [Authorize(RoleEnum.Student)]
        public async Task<ActionResult<CourseEnrollDTO>> GetCourseEnrollByCourseIds(int? courseId)
        {
            var user = (User)HttpContext.Items["User"];
            return Ok(_service.GetCourseEnrollByCourseIdAsync(courseId, user.UserId));
        }

        [HttpGet("getExamCandidate/{courseId}/{lessonNum}")]
        [Authorize(RoleEnum.Student)]
        public async Task<ActionResult<CourseEnrollDTO>> GetExamCandiate(int? courseId, int? lessonNum)
        {
            var user = (User)HttpContext.Items["User"];

            var courseenroll = await _context.CourseEnrolls
                .Include(c => c.User).Include(c => c.Course).ThenInclude(ce => ce.Lessons)
                .Where(ce => ce.CourseId == courseId && ce.UserId == user.UserId).FirstOrDefaultAsync();

            var lesson = await _context.Lessons.Include(l => l.QuestionBank).Where(q => q.LessonNum == lessonNum && q.CourseId == courseId).FirstOrDefaultAsync();
            if (lesson != null)
            {
                if (lesson.QuestionBank != null)
                {
                    var examcandidate = _context.ExamCandidates.Where(e => e.CandidateId == user.UserId && e.QuestionBankId == lesson.QuestionBank.QuestionBankId).FirstOrDefault();
                    if (examcandidate == null)
                    {
                        var typeExam = "quiz";
                        var lessonsInACourse = await _context.Lessons.Where(l => l.CourseId == lesson.CourseId).OrderBy(l => l.LessonNum).ToListAsync();
                        if (lessonsInACourse.Last().LessonId == lesson.LessonId) typeExam = "finalquiz";
                        var newExamCandidate = new ExamCandidate()
                        {
                            CandidateId = user.UserId,
                            QuestionBankId = lesson.QuestionBank.QuestionBankId,
                            StartExamDate = DateTime.Now,
                            TypeExam = typeExam
                        };
                        await _context.ExamCandidates.AddAsync(newExamCandidate);
                        await _context.SaveChangesAsync();

                        courseenroll.Quiz += string.IsNullOrEmpty(courseenroll.Quiz) ? newExamCandidate.ExamCandidateId.ToString() : ";" + newExamCandidate.ExamCandidateId.ToString();
                        _context.CourseEnrolls.Update(courseenroll);
                        await _context.SaveChangesAsync();
                        return Ok(newExamCandidate.ExamCandidateId);
                    }
                    else return Ok(examcandidate.ExamCandidateId);
                }
            }
            return Ok(0);
        }

        [HttpGet("getcourseenrollsbycourseid/{courseId}")]
        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        public async Task<ActionResult<CourseEnrollDTO>> GetCourseEnrollsByCourseIds(int? courseId)
        {
            return Ok(_service.GetCourseEnrollsByCourseIdAsync(courseId));
        }

        [HttpPost]
        public async Task<IActionResult> PostCourseEnroll([FromBody] CourseEnrollDTO courseEnrollDto)
        {
            await _service.AddCourseEnrollAsync(courseEnrollDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseEnroll(int id, [FromBody] CourseEnrollDTO courseEnrollDto)
        {
            await _service.UpdateCourseEnrollAsync(id, courseEnrollDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseEnroll(int id)
        {
            await _service.DeleteCourseEnrollAsync(id);
            return NoContent();
        }
    }
}
