using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.DTOs.IELTS;
using ASPNET_API.Application.Services.Interfa;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Interface.Repositories;
using ASPNET_API.Infrastructure.Data;
using ASPNET_API.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace ASPNET_API.Application.Services
{
    public class CourseService
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;
        private readonly CourseRepository _courseRepository;

        public CourseService(DonationWebApp_v2Context context, IMapper mapper, CourseRepository courseRepository)
        {
            _context = context;
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<List<CourseDTO>> GetRelatedCoursesAsync(int id)
        {

            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == id);
            if (course == null){ return null;}

            var courses = _context.Courses
                .Include(e => e.CourseEnrolls)
                .Include(c => c.Category)
                .Include(c => c.Lessons)
                .Where(c => c.CategoryId == course.CategoryId)
                .AsEnumerable()
                .OrderByDescending(c => (c.CourseEnrolls ?? new List<CourseEnroll>()).Count)
                .Take(4)
                .ToList();

            return _mapper.Map<List<CourseDTO>>(courses);
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId)
        {
            return await _courseRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Course>> GetByLecturerIdAsync(int lecturerId)
        {
            return await _courseRepository.GetByLecturerIdAsync(lecturerId);
        }

        public async Task AddAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }


        public async Task DeleteAsync(int id)
        {
            await _courseRepository.DeleteAsync(id);
        }



        public async Task<string> ViewLessonAsync(int? courseId, int? lessonNum, User user)
        {
            if (courseId == null) return "/Error/Error404";

            var courseInfo = await _context.Courses.FindAsync(courseId);
            if (courseInfo == null) return $"/Courses/Detail?courseId={courseId}&statuscode=4";

            var courseEnroll = await _context.CourseEnrolls
                .Where(ce => ce.CourseId == courseId && ce.UserId == user.UserId)
                .FirstOrDefaultAsync();

            var lessonInfo = await _context.Lessons
                .Where(l => l.LessonNum == lessonNum && l.CourseId == courseId)
                .FirstOrDefaultAsync();

            if (courseEnroll != null)
            {
                if (courseEnroll.LessonCurrent < lessonNum) return $"/Courses/lesson?courseId={courseId}&lessonNum={courseEnroll.LessonCurrent}";
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
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return $"/Courses/Detail?courseId={courseId}&statuscode=3";
                }
            }

            return "none";
        }



        public async Task<int> GradeAsync(List<string> answers, User user)
        {
            if (answers == null || answers.Count == 0) throw new ArgumentException("Không tìm thấy câu trả lời!");

            var answerInfo = answers.First().Split('-');
            int courseId = int.Parse(answerInfo[1]);
            int lessonId = int.Parse(answerInfo[2]);

            var lessonInfo = await _context.Lessons.FirstOrDefaultAsync(l => l.LessonId == lessonId);
            if (lessonInfo == null) throw new Exception("Bài học không tồn tại!");

            var quizes = JsonSerializer.Deserialize<List<QuizToGradeDTO>>(_mapper.Map<LessonDTO>(lessonInfo).Quiz);
            int result = 0;

            foreach (var l in answers)
            {
                var temp = l.Split('-');
                int index = quizes.FindIndex(q => q.questionNo == int.Parse(temp[3]));

                if (index >= 0 && quizes[index].correctAnswer.Equals(temp[4]))
                {
                    result += 1;
                }
            }

            var courseEnroll = await _context.CourseEnrolls
                .SingleOrDefaultAsync(c => c.CourseId == courseId && c.UserId == user.UserId);

            if (courseEnroll == null) throw new Exception("Người dùng chưa đăng ký khóa học!");


            if (string.IsNullOrEmpty(courseEnroll.Grade)) courseEnroll.Grade = $"{result}";
            else courseEnroll.Grade += $";{result}";


            // Cập nhật trạng thái khóa học nếu đây là bài học cuối cùng
            var lastLesson = await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderByDescending(l => l.LessonNum)
                .FirstOrDefaultAsync();

            if (lastLesson != null 
                && lessonId == lastLesson.LessonId)  courseEnroll.CourseStatus = 1;

            _context.CourseEnrolls.Update(courseEnroll);
            await _context.SaveChangesAsync();

            return result;
        }



        public async Task<List<CourseDTO>> FilterCoursesAsync(string? name, int categoryId)
        {
            var courses = await _context.Courses
                .Where(c => (string.IsNullOrEmpty(name) || c.Name.Contains(name)) &&
                            (categoryId == 0 || c.CategoryId == categoryId))
                .ToListAsync();

            return _mapper.Map<List<CourseDTO>>(courses);
        }


        public async Task<CourseDTO> PostCourseAsync(CourseModel course)
        {
            if (course.Image == null || course.Image.Length == 0)
                throw new Exception("No file uploaded");

            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, course.Image.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await course.Image.CopyToAsync(stream);
            }

            var newCourse = new Course()
            {
                CategoryId = course.CategoryId,
                Name = course.Name,
                Image = $"{_configuration["URL:BackendURL"]}/uploads/{course.Image.FileName}",
                Description = course.Description,
                IsPrivate = course.IsPrivate,
                Price = course.Price,
                LecturerId = course.LecturerId,
                UpdatedAt = DateTime.Now
            };

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseDTO>(newCourse);
        }



        public async Task<CourseDTO> UpdateCourseAsync(int id, CourseModel course)
        {
            if (id != course.CourseId)
                throw new ArgumentException("Course ID mismatch!");

            var checkCourse = await _context.Courses.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.CourseId == id);
            if (checkCourse == null)
                throw new KeyNotFoundException("Không tìm thấy khóa học!");

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

            checkCourse.CategoryId = course.CategoryId;
            checkCourse.Name = course.Name;
            checkCourse.Image = 
                string.IsNullOrEmpty(fileName) ? checkCourse.Image : $"{_configuration["URL:BackendURL"]}/uploads/{fileName}";
            checkCourse.Description = course.Description;
            checkCourse.IsPrivate = course.IsPrivate;
            checkCourse.Price = course.Price;
            checkCourse.IsDelete = course.IsDelete;
            checkCourse.LecturerId = course.LecturerId;
            checkCourse.UpdatedAt = DateTime.Now;

            _context.Courses.Update(checkCourse);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseDTO>(checkCourse);
        }


        public async Task<string> UploadCandidatesExcelAsync(UpdateStudent updateStudent)
        {
            if (_context.ExamCandidates == null)
                throw new Exception("Entity set 'DonationWebApp_v2Context.ExamCandidates' is null.");

            List<CourseEnroll> students = new List<CourseEnroll>();
            List<User> emails = new List<User>();

            if (updateStudent.FileUploads != null && updateStudent.FileUploads.Length != 0)
            {
                using (var stream = new MemoryStream())
                {
                    await updateStudent.FileUploads.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheetCandidates = package.Workbook.Worksheets["Candidates"];
                        if (worksheetCandidates != null)
                        {
                            var rowcount = worksheetCandidates.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                bool isEmail = Regex.IsMatch(worksheetCandidates.Cells[row, 2].Value?.ToString()?.Trim().ToLower(),
                                    @"^(?:(?:[^<>()[\]\\.,;:\s@""]+(?:\.[^<>()[\]\\.,;:\s@""]+)*)|(?:"".+""))@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$");
                                if (isEmail)
                                {
                                    emails.Add(new User
                                    {
                                        Email = worksheetCandidates.Cells[row, 2].Value?.ToString()?.Trim().ToLower(),
                                        FirstName = worksheetCandidates.Cells[row, 3].Value?.ToString()?.Trim().ToLower(),
                                        LastName = worksheetCandidates.Cells[row, 4].Value?.ToString()?.Trim().ToLower()
                                    });
                                }
                                else
                                {
                                    throw new Exception($"Vui lòng xem lại trang tính {worksheetCandidates.Name}, dòng {row}, nhập sai dữ liệu!");
                                }
                            }
                        }
                    }
                }

                var users = await _context.Users.ToListAsync();
                var unregisteredAccount = emails.Where(email => !users.Any(user => user.Email.Equals(email.Email, StringComparison.OrdinalIgnoreCase))).ToList();

                foreach (var item in unregisteredAccount)
                {
                    var newUser = new User
                    {
                        UserName = item.Email,
                        Password = BCryptNet.HashPassword("12345678@"),
                        FirstName = item.FirstName ?? "UserFirstName",
                        LastName = item.LastName ?? "UserLastName",
                        Phone = "",
                        Address = "",
                        Email = item.Email,
                        EnrollDate = DateTime.Now,
                        Active = true
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    var userRole = new UserRole
                    {
                        RoleId = 4,
                        UserId = newUser.UserId
                    };
                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();
                }

                var registeredExam = await _context.CourseEnrolls.Include(e => e.User)
                    .Where(e => e.CourseId == updateStudent.CourseId)
                    .ToListAsync();

                var unregisteredExamEmail = emails
                    .Where(email => !registeredExam.Any(e => e.User.Email.Equals(email.Email, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                foreach (var item in unregisteredExamEmail)
                {
                    var newUser = await _context.Users
                        .Where(u => u.Email.Equals(item.Email, StringComparison.OrdinalIgnoreCase))
                        .SingleOrDefaultAsync();

                    students.Add(new CourseEnroll
                    {
                        CourseId = updateStudent.CourseId,
                        UserId = newUser.UserId
                    });
                }

                await _context.CourseEnrolls.AddRangeAsync(students);
                await _context.SaveChangesAsync();
                return "Upload successful!";
            }

            throw new Exception("Upload your excel file to generate QuestionBank!");
        }



        public async Task<IEnumerable<LectureDto>> GetAllLecturesAsync()
        {
            var lectures = await _context.Users
                .Include(x => x.UserRoles)
                .Where(x => x.UserRoles.Any(ur => ur.RoleId == 3))
                .Select(x => new LectureDto
                {
                    LecturerId = x.UserId,
                    UserName = x.LastName + " " + x.FirstName,
                    Description = x.Description,
                    Image = x.Image
                })
                .ToListAsync();

            return lectures;
        }



        public async Task<ActionResult> GetQuestionAsync(int lessonId, string question, User sessionUser)
        {
            if (_context.QuestionBanks == null || _context.ExamCandidates == null)
            {
                return new NotFoundResult();
            }

            var courseEnroll = await _context.CourseEnrolls
                .Include(c => c.Course)
                .ThenInclude(c => c.Lessons)
                .Where(c => c.UserId == sessionUser.UserId && c.Course.Lessons.Any(l => l.LessonId == lessonId))
                .ToListAsync();

            if (!courseEnroll.Any())
            {
                return new NotFoundObjectResult("Test information could not be found, or you are not on the test list!");
            }

            var lesson = await _context.Lessons
                .Include(l => l.QuestionBank)
                .Where(l => l.LessonId == lessonId)
                .SingleOrDefaultAsync();

            if (lesson?.QuestionBank == null || lesson.QuestionBank.IsClosed ||
                !(lesson.QuestionBank.StartDate <= DateTime.Now && DateTime.Now <= lesson.QuestionBank.EndDate))
            {
                return new BadRequestObjectResult("Rất tiếc, hiện tại đề thi đang đóng!");
            }

            var response = new { QuestionBankId = lesson.QuestionBankId };

            return question.ToLower() switch
            {
                "reading" => new OkObjectResult(new { response.QuestionBankId, readingJSON = lesson.QuestionBank.ReadingJSON }),
                "listening" => new OkObjectResult(new { response.QuestionBankId, listeningJSON = lesson.QuestionBank.ListeningJSON }),
                _ => new OkObjectResult(new { response.QuestionBankId, writingJSON = lesson.QuestionBank.WritingJSON })
            };
        }



        public async Task<ActionResult> Grade(int courseEnrollId, int lessonId, SubmitedAnswersDTO submitedAnswers, User sessionUser)
        {
            var questionBank = await _context.QuestionBanks.FindAsync(submitedAnswers.QuestionBankId);
            if (questionBank == null) return new NotFoundObjectResult("Your QuestionBank is not found!");

            var courseEnroll = await _context.CourseEnrolls
                .SingleOrDefaultAsync(e => e.UserId == sessionUser.UserId && e.CourseEnrollId == courseEnrollId);
            if (courseEnroll == null)
            {
                return new NotFoundObjectResult("Test information could not be found, or you are not on the test list!");
            }

            try
            {
                var hashSetB = new HashSet<(string, string)>(submitedAnswers.Answers.Select(b => (b.QuestionNo, b.SubmitedAnswer)));
                int intersectCount = 0;
                var lesson = await _context.Lessons.FindAsync(lessonId);

                switch (submitedAnswers.ForQuestion.ToLower())
                {
                    case "reading":
                        intersectCount = CalculateCorrectAnswers(questionBank.ReadingJSON, hashSetB);
                        await UpdateGrade(courseEnroll, lesson, intersectCount, "reading");
                        break;

                    case "listening":
                        intersectCount = CalculateCorrectAnswers(questionBank.ListeningJSON, hashSetB);
                        await UpdateGrade(courseEnroll, lesson, intersectCount, "listening");
                        break;

                    case "writing":
                        // TODO: Implement writing grading logic
                        break;

                    default:
                        return new BadRequestObjectResult("Unexpected Error, Please try again!");
                }

                _context.CourseEnrolls.Update(courseEnroll);
                await _context.SaveChangesAsync();

                return new OkObjectResult("Your questions have been saved!");
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Unexpected Error, Please try again!");
            }
        }

        private int CalculateCorrectAnswers(dynamic questionJSON, HashSet<(string, string)> submittedAnswers)
        {
            var allQuestions = questionJSON?.Parts.SelectMany(p => p.Groups)
                        .SelectMany(g => g.Questions)
                        .Select(q => new { q.QuestionNo, q.CorrectAnswer })
                        .ToList();
            return allQuestions.Count(a => submittedAnswers.Contains((a.QuestionNo, a.CorrectAnswer)));
        }

        private async Task UpdateGrade(CourseEnroll courseEnroll, Lesson lesson, int intersectCount, string questionType)
        {
            string newGrade = $"{lesson.LessonNum}:{await _questionBankService.CalculateBandScore(intersectCount, questionType)}";
            courseEnroll.Grade = string.IsNullOrEmpty(courseEnroll.Grade)
                ? newGrade
                : $"{courseEnroll.Grade};{newGrade}";
        }

    }
}
