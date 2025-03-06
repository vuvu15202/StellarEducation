using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Models.DTO;
using ASPNET_API.Models.Entity;
using ASPNET_API.temp;
using ASPNET_API.Authorization;
using ASPNET_API.Models;

namespace ASPNET_API.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseEnrollsController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;

        public CourseEnrollsController(DonationWebApp_v2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // GET: api/CourseEnrolls
        [HttpGet]
        public async Task<ActionResult<List<CourseEnrollDTO>>> GetCourseEnrolls()
        {
          if (_context.CourseEnrolls == null)
          {
              return NotFound();
          }
            var courses = await _context.CourseEnrolls.Include(c => c.User).Include(c => c.Course).ToListAsync();
            return Ok(_mapper.Map<List<CourseEnrollDTO>>(courses));
        }

        // GET: api/CourseEnrolls
        [HttpGet("getcourseenroll/{courseId}")]
        [Authorize(RoleEnum.Student)]
        public async Task<ActionResult<CourseEnrollDTO>> GetCourseEnrollByCourseIds(int? courseId)
        {
            var user = (User)HttpContext.Items["User"];
            if (_context.CourseEnrolls == null)
            {
                return NotFound();
            }
            var courses = await _context.CourseEnrolls
                .Include(c => c.User).Include(c => c.Course).ThenInclude(ce => ce.Lessons)
                .Where(ce => ce.CourseId == courseId && ce.UserId == user.UserId).FirstOrDefaultAsync();
            return Ok(_mapper.Map<CourseEnrollDTO>(courses));
        }

        // GET: api/CourseEnrolls
        [HttpGet("getExamCandidate/{courseId}/{lessonNum}")]
        [Authorize(RoleEnum.Student)]
        public async Task<ActionResult<CourseEnrollDTO>> GetExamCandiate(int? courseId, int? lessonNum)
        {
            var user = (User)HttpContext.Items["User"];
            if (_context.CourseEnrolls == null)
            {
                return NotFound();
            }
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


        // GET: api/CourseEnrolls
        [HttpGet("getcourseenrollsbycourseid/{courseId}")]
        [Authorize(RoleEnum.Admin,RoleEnum.Staff, RoleEnum.Lecturer)]
        public async Task<ActionResult<CourseEnrollDTO>> GetCourseEnrollsByCourseIds(int? courseId)
        {
            if (_context.CourseEnrolls == null)
            {
                return NotFound();
            }
            var courses = await _context.CourseEnrolls
                .Include(c => c.User)
                .Where(ce => ce.CourseId == courseId).ToListAsync();
            return Ok(_mapper.Map<List<CourseEnrollDTO>>(courses));
        }
        // GET: api/CourseEnrolls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseEnroll>> GetCourseEnroll(int id)
        {
          if (_context.CourseEnrolls == null)
          {
              return NotFound();
          }
            var courseEnroll = await _context.CourseEnrolls.FindAsync(id);

            if (courseEnroll == null)
            {
                return NotFound();
            }

            return courseEnroll;
        }

        // PUT: api/CourseEnrolls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseEnroll(int id, [FromBody]CourseEnroll courseEnroll)
        {
            if (id != courseEnroll.CourseEnrollId)
            {
                return BadRequest();
            }

            //_context.Entry(courseEnroll).State = EntityState.Modified;
            var oldCE = await _context.CourseEnrolls
                .Include(c => c.User).Include(c => c.Course).ThenInclude(ce => ce.Lessons).ThenInclude(ce => ce.QuestionBank)
                .Where(ce => ce.CourseId == courseEnroll.CourseId && ce.UserId == courseEnroll.UserId).FirstOrDefaultAsync();

            try
            {
                
                oldCE.LessonCurrent = courseEnroll.LessonCurrent;
                _context.Update(oldCE);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseEnrollExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_mapper.Map<CourseEnrollDTO>(oldCE));
        }

        // POST: api/CourseEnrolls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseEnroll>> PostCourseEnroll(CourseEnroll courseEnroll)
        {
          if (_context.CourseEnrolls == null)
          {
              return Problem("Entity set 'DonationWebApp_v2Context.CourseEnrolls'  is null.");
          }
            _context.CourseEnrolls.Add(courseEnroll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseEnroll", new { id = courseEnroll.CourseEnrollId }, courseEnroll);
        }

        // DELETE: api/CourseEnrolls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseEnroll(int id)
        {
            if (_context.CourseEnrolls == null)
            {
                return NotFound();
            }
            var courseEnroll = await _context.CourseEnrolls.FindAsync(id);
            if (courseEnroll == null)
            {
                return NotFound();
            }

            _context.CourseEnrolls.Remove(courseEnroll);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseEnrollExists(int id)
        {
            return (_context.CourseEnrolls?.Any(e => e.CourseEnrollId == id)).GetValueOrDefault();
        }
    }
}
