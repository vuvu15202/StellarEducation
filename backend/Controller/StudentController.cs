using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Application.Services;
using ASPNET_API.Domain.Entities;
using ASPNET_API.Infrastructure.Data;

namespace ASPNET_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CourseEnrollService _coursesErollService;
        private readonly DonationWebApp_v2Context _context;

        public StudentController(CourseEnrollService coursesErollService, DonationWebApp_v2Context context)
        {
            _coursesErollService = coursesErollService;
            _context = context;
        }

        [HttpGet("GetCourseByUserId")]
        public async Task<IActionResult> GetCourseByUserId()
        {
            var student = (User)HttpContext.Items["User"];
            var courses = await _coursesErollService.GetByUserIdAsync(student.UserId);
            var coursesdto = courses.Select(
                ce => new CourseEnrollDTOs
                {
                    Id = ce.CourseEnrollId,
                    CourseId = ce.Course.CourseId,
                    StudentId = ce.UserId,
                    CourseName = ce.Course.Name,
                    EnrollDate = ce.EnrollDate.ToString("dd/MM/yyyy"),
                    EndDate = ce.EnrollDate.AddMonths(3).ToString("dd/MM/yyyy"),
                    CourseStatus = ce.CourseStatus,
                    Grade = ce.Grade,
                    AverageGrade = ce.AverageGrade,
                    StudentFeeId = ce.StudentFeeId
                }
            );
            return Ok(courses);
        }

        [HttpGet("GetCourseDetailById")]
        public async Task<IActionResult> GetCourseEnrollById(int courseEnrollId)
        {
            var course = await _coursesErollService.GetCourseEnrollByIdAsync(courseEnrollId);
            var examcanIds = string.IsNullOrEmpty(course.Quiz) ? new string[0] : course.Quiz.Split(";");
            var examcandidates = await _context.ExamCandidates.Where(e => !string.IsNullOrEmpty(course.Quiz) && examcanIds.Contains(e.ExamCandidateId.ToString())).ToListAsync();
            var courseDTO = new CourseEnrollDTOs
            {
                Id = course.CourseEnrollId,
                CourseId = course.Course.CourseId,
                StudentId = course.UserId,
                CourseName = course.Course.Name,
                EnrollDate = course.EnrollDate.ToString("dd/MM/yyyy"),
                EndDate = course.EnrollDate.AddMonths(3).ToString("dd/MM/yyyy"),
                CourseStatus = course.CourseStatus,
                Grade = course.Grade,
                AverageGrade = course.AverageGrade,
                StudentFeeId = course.StudentFeeId,
                examCandidates = examcandidates
            };
            return Ok(courseDTO);
        }
    }


}
