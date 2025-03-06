using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ASPNET_API.Authorization;
using ASPNET_API.Models;
using ASPNET_API.Models.DTO;
using static System.Net.Mime.MediaTypeNames;
using OfficeOpenXml;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Text.RegularExpressions;
using ASPNET_API.Models.Entity.TESTIELTS;
using ASPNET_API.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_API.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly IQuestionBankService _questionBankService;

        public CoursesController(DonationWebApp_v2Context context, IMapper mapper, IWebHostEnvironment environment, IConfiguration configuration, IQuestionBankService questionBankService)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
            _configuration = configuration;
            _questionBankService = questionBankService;
        }

        [AllowAnonymous]
        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var courses = _context.Courses.Include(u => u.Lecturer).Include(e => e.CourseEnrolls).Include(c => c.Lessons).Include(c => c.Category).AsEnumerable()
                .OrderByDescending(c => (c.CourseEnrolls ?? new List<CourseEnroll>()).Count);
            return Ok(_mapper.Map<List<CourseDTO>>(courses));
        }

        [AllowAnonymous]
        [HttpGet("relatedcourse/{id}")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetRelatedCourses(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            var courses = _context.Courses
                .Include(e => e.CourseEnrolls)
                .Include(c => c.Category)
                .Include(c => c.Lessons)
                .Where(c => c.CategoryId == course.CategoryId)
                .AsEnumerable()
                .OrderByDescending(c => (c.CourseEnrolls ?? new List<CourseEnroll>()).Count)//    .OrderByDescending(c => c.CourseEnrolls != null ? c.CourseEnrolls.Count : 0)
                .Take(4);
            return Ok(_mapper.Map<List<CourseDTO>>(courses));
        }

        // GET: api/Courses/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses
                .Include(u => u.Lecturer)
                .Include(e => e.CourseEnrolls)
                .Include(c => c.Lessons)
                .Include(c => c.Category)
                .SingleOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDTO>(course));
        }

        

        [Authorize(RoleEnum.Student)]
        [HttpGet("ViewLesson")]
        public async Task<ActionResult> ViewLesson(int? courseId, int? lessonNum = 1)
        {
            if (courseId == null)
            {
                return Ok("/Error/Error404");
            }
            var courseInfo = await _context.Courses.FindAsync(courseId);
            if (courseInfo == null)
            {
                return Ok($"/Courses/Detail?courseId={courseId}&statuscode=4");
            }


            var user = (User)HttpContext.Items["User"];
            var courseEnroll = await _context.CourseEnrolls
                .Where(ce => ce.CourseId == courseId && ce.UserId == user.UserId).FirstOrDefaultAsync();
            var lessonInfo = await _context.Lessons
                .Where(l => l.LessonNum == lessonNum && l.CourseId == courseId).FirstOrDefaultAsync();
            if (courseEnroll != null)
            {
                if (courseEnroll.LessonCurrent < lessonNum)
                {
                    return Ok($"/Courses/lesson?courseId={courseId}&lessonNum={courseEnroll.LessonCurrent}");
                }
            }
            else
            {
                if (courseInfo.Price == 0)
                {
                    CourseEnroll newCourseEnroll = new CourseEnroll()
                    {
                        UserId = user.UserId,
                        CourseId = (int)courseId,
                        EnrollDate = DateTime.Now,
                        LessonCurrent = 1,
                        CourseStatus = 0,
                    };
                    _context.CourseEnrolls.Add(newCourseEnroll);
                    _context.SaveChanges();

                }
                else
                {
                    return Ok($"/Courses/Detail?courseId={courseId}&statuscode=3");
                }


            }

            return Ok("none");
        }


        [Authorize(RoleEnum.Student)]
        [HttpPost("grade")]
        public async Task<ActionResult> Grade([FromBody] List<string> answer) //question-2-5-1-B =      question-courseId-lessonId-questionNo-B
        {
            if (answer == null || answer.Count == 0)
            {
                return Problem("không tìm thấy câu trả lời!");
            }
            var answerInfo = answer.First().Split('-');
            var lessonInfo = await _context.Lessons.FirstOrDefaultAsync(l => l.LessonId == Int16.Parse(answerInfo[2]));
            var quizes = JsonSerializer.Deserialize<List<QuizToGradeDTO>>(_mapper.Map<LessonDTO>(lessonInfo).Quiz);
            int result = 0;
            foreach (var l in answer)
            {
                var temp = l.Split('-');
                int idex = quizes.FindIndex(q => q.questionNo == Int16.Parse(temp[3]));
                if (quizes[idex].correctAnswer.Equals(temp[4]))
                {
                    result += 1;
                }

            }

            var user = (User)HttpContext.Items["User"];
            var coursenroll = _context.CourseEnrolls
                .Where(c => c.CourseId == Int16.Parse(answerInfo[1]) && c.UserId == user.UserId).SingleOrDefault();

            if (String.IsNullOrEmpty(coursenroll!.Grade))
            {
                coursenroll.Grade = $"{result}";

            }
            else
            {
                coursenroll.Grade = coursenroll.Grade + $";{result}";
            }
            //update grade
            _context.CourseEnrolls.Update(coursenroll);

            //update course status
            var checkLesson = _context.Lessons.Where(l => l.CourseId == Int16.Parse(answerInfo[1])).OrderBy(l => l.LessonNum).LastOrDefault();
            if (lessonInfo.LessonId == checkLesson.LessonId)
            {
                coursenroll.CourseStatus = 1;
            }
            _context.SaveChanges();

            return Ok(new { result = $"{result}" });
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            course.IsDelete = true;
            _context.Courses.Update(course);
            //_context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult> Filter(string? name, int categoryId = 0)
        {

            var courses = await _context.Courses
                                        .Where(c => (string.IsNullOrEmpty(name) || c.Name.Contains(name))
                                        && (categoryId == 0 || c.CategoryId == categoryId))
                                        .ToListAsync();
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
            if (course.Image == null || course.Image.Length == 0)
                return BadRequest("No file uploaded");

            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, course.Image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await course.Image.CopyToAsync(stream);
            }

            var cour = new Course()
            {
                CategoryId = course.CategoryId,
                Name = course.Name,
                Image = _configuration["URL:BackendURL"] + "/uploads/" + course.Image.FileName,
                Description = course.Description,
                IsPrivate = course.IsPrivate,
                Price = course.Price,
                LecturerId = course.LecturerId,
                UpdatedAt = DateTime.Now
            };
            _context.Courses.Add(cour);
            await _context.SaveChangesAsync();

            // Xử lý nội dung JSON từ fileContentJson ở đây
            // Ví dụ: bạn có thể in nội dung của JSON ra console để kiểm tra
            //System.Diagnostics.Debug.WriteLine("test upload");

            return Ok(cour);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, [FromForm] CourseModel course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            var checkCourse = _context.Courses.IgnoreQueryFilters().FirstOrDefault(c => c.CourseId == id);
            if (checkCourse == null)
            {
                return NotFound("Không tìm thấy khóa học!");
            }

            string fileName = "";
            if (course.Image != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var filePath = Path.Combine(uploads, course.Image.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await course.Image.CopyToAsync(stream);
                    fileName = course.Image.FileName;
                }
            }

            try
            {
                checkCourse.CategoryId = course.CategoryId;
                checkCourse.Name = course.Name;
                checkCourse.Image = String.IsNullOrEmpty(fileName) ? checkCourse.Image : _configuration["URL:BackendURL"] + "/uploads/" + fileName;
                checkCourse.Description = course.Description;
                checkCourse.IsPrivate = course.IsPrivate;
                checkCourse.Price = course.Price;
                checkCourse.IsDelete = course.IsDelete;
                checkCourse.LecturerId = course.LecturerId;
                checkCourse.UpdatedAt = DateTime.Now;

                _context.Update(checkCourse);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(checkCourse);
        }

        [AllowAnonymous]
        [HttpGet("GetAllLecture")]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetAllLecture()
        {
            try
            {
                var lectures = await _context.Users
                .Include(x => x.UserRoles)
                .Where(x => x.UserRoles.Any(ur => ur.RoleId == 3))
                .Select(x => new LectureDto
                {
                    LecturerId = x.UserId,
                    UserName =  x.LastName + " " + x.FirstName ,
                    Description = x.Description,
                    Image = x.Image
                })
                .ToListAsync();

                if (lectures.Any())
                {
                    return Ok(lectures);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }


        [HttpPost("UploadStudentsExcel")]
        public async Task<ActionResult> UploadCandidatesExcel([FromForm] UpdateStudent updateStudent)
        {
            if (_context.ExamCandidates == null)
            {
                return Problem("Entity set 'DonationWebApp_v2Context.ExamCandidates'  is null.");
            }
            List<CourseEnroll> students = new List<CourseEnroll>();
            List<User> emails = new List<User>();

            if (updateStudent.FileUploads != null && updateStudent.FileUploads.Length != 0)
            {
                using (var stream = new MemoryStream())
                {
                    await updateStudent.FileUploads.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var workbook = package.Workbook;

                        var worksheetCandidates = workbook.Worksheets["Candidates"];
                        if (worksheetCandidates != null)
                        {
                            var rowcount = worksheetCandidates.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                try
                                {
                                    bool isEmail = Regex.IsMatch(worksheetCandidates.Cells[row, 2].Value?.ToString()?.Trim().ToLower(),
                                        @"^(?:(?:[^<>()[\]\\.,;:\s@""]+(?:\.[^<>()[\]\\.,;:\s@""]+)*)|(?:"".+""))@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$");
                                    if (isEmail) emails.Add(new Models.Entity.User()
                                    {
                                        Email = worksheetCandidates.Cells[row, 2].Value?.ToString()?.Trim().ToLower(),
                                        FirstName = worksheetCandidates.Cells[row, 3].Value?.ToString()?.Trim().ToLower(),
                                        LastName = worksheetCandidates.Cells[row, 4].Value?.ToString()?.Trim().ToLower()
                                    });
                                    else throw new Exception($"Vui lòng xem lại trang tính {worksheetCandidates.Name}, dòng {row}, nhập sai dữ liệu!");
                                }
                                catch (Exception ex)
                                {
                                    //throw new Exception($"Xem lại trang tính {worksheetParts.Name}, dòng {row}");
                                    return BadRequest(ex.Message);
                                }
                            }
                        }
                    }
                }
                var users = await _context.Users.ToListAsync();
                var unregisteredAccount = emails.Where(email => !users.Any(user => user.Email.Equals(email.Email, StringComparison.OrdinalIgnoreCase))).ToList();
                foreach (var item in unregisteredAccount)
                {
                    try
                    {
                        var newUser = new User();
                        newUser.UserName = item.Email;
                        newUser.Password = BCryptNet.HashPassword("12345678@");
                        newUser.FirstName = item.FirstName == null ? "UserFirstName" : item.FirstName;
                        newUser.LastName = item.LastName == null ? "UserLastName" : item.LastName;
                        newUser.Phone = "";
                        newUser.Address = "";
                        newUser.Email = item.Email;
                        newUser.EnrollDate = DateTime.Now;
                        newUser.Active = true;

                        _context.Users.Add(newUser);
                        _context.SaveChanges();

                        var userRole = new UserRole();
                        userRole.RoleId = 4;
                        userRole.UserId = newUser.UserId;
                        _context.UserRoles.Add(userRole);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.InnerException);
                    }
                }

                var registeredExam = await _context.CourseEnrolls.Include(e => e.User).Where(e => e.CourseId == updateStudent.CourseId).ToListAsync();
                var unregisteredExamEmail = emails.Where(email => !registeredExam.Any(e => e.User.Email.ToLower().Equals(email.Email.ToLower()))).ToList();
                foreach (var item in unregisteredExamEmail)
                {
                    var newUser = await _context.Users.Where(u => u.Email.ToLower().Equals(item.Email.ToLower())).SingleOrDefaultAsync();
                    students.Add(new CourseEnroll()
                    {
                        CourseId = updateStudent.CourseId,
                        UserId = newUser.UserId,

                    });
                }
                await _context.CourseEnrolls.AddRangeAsync(students);
                await _context.SaveChangesAsync();
                return Ok("Successful!");

            }
            else
            {
                throw new Exception($"Upload your excel file to generate QuestionBank!");
            }

            //_context.ExamCandidates.AddRange(examCandidate);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetExamCandidate", new { id = examCandidate.ExamCandidateId }, examCandidate);
            return Ok();
        }

        [HttpGet("{lessonId}/{question}")]
        public async Task<ActionResult> GetQuestion(int lessonId, string question)
        {
            if (_context.QuestionBanks == null || _context.ExamCandidates == null)
            {
                return NotFound();
            }
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
        public async Task<ActionResult> Grade(int courseEnrollId, int lessonId, [FromBody]SubmitedAnswersDTO submitedAnswers)
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
                        courseEnroll.Grade = courseEnroll.Grade + ";" + lesson.LessonNum + ":" +await _questionBankService.CalculateBandScore(intersectCount, "reading");
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
