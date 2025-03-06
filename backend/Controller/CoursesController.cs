using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.DTOs.IELTS;
using ASPNET_API.Application.Services;
using ASPNET_API.Authorization;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ASPNET_API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;
        private readonly QuestionBankService1 _questionBankService;
        private readonly DonationWebApp_v2Context _context;

        public CoursesController(CourseService courseService, QuestionBankService1 questionBankService, DonationWebApp_v2Context context)
        {
            _courseService = courseService;
            _questionBankService = questionBankService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }



        [AllowAnonymous]
        [HttpGet("relatedcourse/{id}")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetRelatedCourses(int id)
        {
            var courses = await _courseService.GetRelatedCoursesAsync(id);

            if (courses == null) return NotFound();

            return Ok(courses);
        }



        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var courses = await _courseService.GetByCategoryIdAsync(categoryId);
            return Ok(courses);
        }



        [HttpGet("lecturer/{lecturerId}")]
        public async Task<IActionResult> GetByLecturerId(int lecturerId)
        {
            var courses = await _courseService.GetByLecturerIdAsync(lecturerId);
            return Ok(courses);
        }



        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            await _courseService.AddAsync(course);
            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Course course)
        {
            if (id != course.CourseId) return BadRequest();
            await _courseService.UpdateAsync(course);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
            return NoContent();
        }



        [Authorize(RoleEnum.Student)]
        [HttpGet("ViewLesson")]
        public async Task<ActionResult> ViewLesson(int? courseId, int? lessonNum = 1)
        {
            var user = (User)HttpContext.Items["User"];
            var result = await _courseService.ViewLessonAsync(courseId, lessonNum, user);
            return Ok(result);
        }



        [Authorize(RoleEnum.Student)]
        [HttpPost("grade")]
        public async Task<ActionResult> Grade([FromBody] List<string> answers)
        {
            try
            {
                var user = (User)HttpContext.Items["User"];
                int result = await _courseService.GradeAsync(answers, user);
                return Ok(new { result = $"{result}" });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult> Filter(string? name, int categoryId = 0)
        {
            var courses = await _courseService.FilterCoursesAsync(name, categoryId);

            if (!courses.Any())
            {
                return NotFound("No courses found.");
            }

            return Ok(courses);
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostCourse([FromForm] CourseModel course)
        {
            try
            {
                var newCourse = await _courseService.PostCourseAsync(course);
                return Ok(newCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, [FromForm] CourseModel course)
        {
            try
            {
                var updatedCourse = await _courseService.UpdateCourseAsync(id, course);
                return Ok(updatedCourse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }


        [AllowAnonymous]
        [HttpGet("GetAllLecture")]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetAllLecture()
        {
            try
            {
                var lectures = await _courseService.GetAllLecturesAsync();
                return Ok(lectures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }


        [HttpPost("UploadStudentsExcel")]
        public async Task<ActionResult> UploadCandidatesExcel([FromForm] UpdateStudent updateStudent)
        {
            try
            {
                string result = await _courseService.UploadCandidatesExcelAsync(updateStudent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{lessonId}/{question}")]
        public async Task<ActionResult> GetQuestion(int lessonId, string question)
        {
            var sessionUser = (User)HttpContext.Items["User"];

            var courseEnroll = await _context.CourseEnrolls
                .Include(c => c.Course)
                .ThenInclude(c => c.Lessons)
                .Where(c => c.UserId == sessionUser.UserId && c.Course.Lessons.Any(l => l.LessonId == lessonId)).ToListAsync();

            if (courseEnroll.Count == 0)
            {
                return NotFound("Test information could not be found, or you are not on the test list!");
            }

            var lesson = await _context.Lessons.Include(l => l.QuestionBank).Where(l => l.LessonId == lessonId).SingleOrDefaultAsync();

            if (lesson.QuestionBank.IsClosed || !(lesson.QuestionBank.StartDate <= DateTime.Now && DateTime.Now <= lesson.QuestionBank.EndDate))
                return BadRequest("Rất tiếc, hiện tại đề thi đang đóng!");

            if (question.ToLower().Equals("reading"))
            {
                return Ok(new { QuestionBankId = lesson.QuestionBankId, readingJSON = lesson.QuestionBank.ReadingJSON });
            }
            else if (question.ToLower().Equals("listening"))
            {
                return Ok(new { QuestionBankId = lesson.QuestionBankId, listeningJSON = lesson.QuestionBank.ListeningJSON });
            }
            else
            {
                return Ok(new { QuestionBankId = lesson.QuestionBankId, writingJSON = lesson.QuestionBank.WritingJSON });
            }
        }


        [HttpPost("Grade/{courseEnrollId}/{lessonId}")]
        public async Task<ActionResult> Grade(int courseEnrollId, int lessonId, [FromBody] SubmitedAnswersDTO submitedAnswers)
        {
            var questionBank = await _context.QuestionBanks.FindAsync(submitedAnswers.QuestionBankId);
            if (questionBank == null) return NotFound("Your QuestionBank is notfound!");

            var sessionUser = (User)HttpContext.Items["User"];
            var courseEnroll = await _context.CourseEnrolls.SingleOrDefaultAsync(e => e.UserId == sessionUser.UserId && e.CourseEnrollId == courseEnrollId);
            if (courseEnroll == null)
            {
                return NotFound("Test information could not be found, or you are not on the test list!");
            }

            try
            {
                var hashSetB = new HashSet<(string, string)>(submitedAnswers.Answers.Select(b => (b.QuestionNo, b.SubmitedAnswer)));
                int intersectCount = 0;
                var Grade = string.IsNullOrEmpty(courseEnroll.Grade) ? new List<QuizResultDTO>() : JsonSerializer.Deserialize<List<QuizResultDTO>>(courseEnroll.Grade);
                var lesson = await _context.Lessons.FindAsync(lessonId);
                if (submitedAnswers.ForQuestion.ToLower().Equals("reading"))
                {
                    var allQuestions = questionBank.ReadingJSON?.Parts.SelectMany(p => p.Groups)
                                 .SelectMany(g => g.Questions)
                                 .Select(q => new { q.QuestionNo, q.CorrectAnswer })
                                 .ToList();
                    intersectCount = allQuestions.Count(a => hashSetB.Contains((a.QuestionNo, a.CorrectAnswer)));
                    if (String.IsNullOrEmpty(courseEnroll.Grade))
                    {
                        courseEnroll.Grade = lesson.LessonNum + ":" + await _questionBankService.CalculateBandScore(intersectCount, "reading");
                    }
                    else
                    {
                        courseEnroll.Grade = courseEnroll.Grade + ";" + lesson.LessonNum + ":" + await _questionBankService.CalculateBandScore(intersectCount, "reading");
                    }
                }
                else if (submitedAnswers.ForQuestion.ToLower().Equals("listening"))
                {
                    var allQuestions = questionBank.ListeningJSON?.Parts.SelectMany(p => p.Groups)
                                 .SelectMany(g => g.Questions)
                                 .Select(q => new { q.QuestionNo, q.CorrectAnswer })
                                 .ToList();
                    intersectCount = allQuestions.Count(a => hashSetB.Contains((a.QuestionNo, a.CorrectAnswer)));
                    if (String.IsNullOrEmpty(courseEnroll.Grade))
                    {
                        courseEnroll.Grade = lesson.LessonNum + ":" + await _questionBankService.CalculateBandScore(intersectCount, "listening");
                    }
                    else
                    {
                        courseEnroll.Grade = courseEnroll.Grade + ";" + lesson.LessonNum + ":" + await _questionBankService.CalculateBandScore(intersectCount, "listening");
                    }
                }
                else if (submitedAnswers.ForQuestion.ToLower().Equals("writing"))
                {
                    //examcandidate.SubmitedWriting = JsonSerializer.Serialize(submitedAnswers.Answers);
                    //examcandidate.BandScoreWriting = await _questionBankService.CalculateBandScore(intersectCount, "writing");
                }
                else
                {
                    return BadRequest("Unexpected Error, Please try again!");
                }
                _context.CourseEnrolls.Update(courseEnroll);
                await _context.SaveChangesAsync();

                return Ok("Your questions have been saved!.");
            }
            catch (Exception ex)
            {
                return BadRequest("Unexpected Error, Please try again!");
            }
        }
    }
}
