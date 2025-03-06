using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPNET_API.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Domain.Entities.RoleEnum.Admin)]
    public class DashboardController : ControllerBase
    {
        private readonly IMapper _mapper;

        public DashboardController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("report")]
        public IActionResult getreportdashboardd()
        {
            var todayRegister = _context.Users.
                Where(u => u.EnrollDate.Value.Date == DateTime.Now.Date)
                .ToList().Count();

            var todayEnrollCourse = _context.CourseEnrolls.Where(ce => ce.EnrollDate.Date == DateTime.Now.Date).ToList().Count();


            var todayCompleteCourse = _context.CourseEnrolls.Where(c => c.ExpireDate.Date == DateTime.Now.Date).ToList().Count();

            var todayExam = _context.QuestionBanks.Where(q => q.StartDate.Date == DateTime.Now.Date).ToList().Count();

            //var newEnroll = _context.CourseEnrolls.
            //    Where(o => o.EnrollDate.Year == DateTime.Now.Year
            //        && o.EnrollDate.Month == DateTime.Now.Month).ToList().Count();

            var statisticNewUser = getStatisticNewUser(2024);

            return Ok(
                new
                {
                    todayRegister,
                    todayEnrollCourse,
                    todayCompleteCourse,
                    todayExam,
                    statisticNewUser
                });
        }

        [HttpGet("piechart")]
        public async Task<IActionResult> piechart()
        {
            var totalStudent = _context.Users.Include(u => u.UserRoles).ThenInclude(u => u.Role)
                .Where(u => u.UserRoles.Any(r => r.Role.RoleName.ToLower().Equals("student"))).ToList().Count();
            var totalLecturer = _context.Users.Include(u => u.UserRoles).ThenInclude(u => u.Role)
                .Where(u => u.UserRoles.Any(r => r.Role.RoleName.ToLower().Equals("lecturer"))).ToList().Count();
            var totalStaff = _context.Users.Include(u => u.UserRoles).ThenInclude(u => u.Role)
                .Where(u => u.UserRoles.Any(r => r.Role.RoleName.ToLower().Equals("staff"))).ToList().Count();
            return Ok(new double[] { totalStudent, totalLecturer, totalStaff });
        }

        [HttpGet("getStatisticNewUser")]
        public IActionResult getStatisticDashboard(int year = 2024)
        {
            return Ok(new { statisticNewUser = getStatisticNewUser(year) });
        }

        List<int> getStatisticNewUser(int year = 2024)
        {
            var list = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                int value = _context.Users.
                    Where(o => o.EnrollDate.Value.Year == year
                        && o.EnrollDate.Value.Month == i)
                    .ToList().Count();
                list.Add(value);
            }
            return list;
        }

        List<int> getStatistic(int? courseId, int year = 2024)
        {
            var list = new List<int>();
            if (courseId != null)
            {
                for (int i = 1; i <= 12; i++)
                {
                    int value = _context.StudentFees.Include(c => c.CourseEnroll).
                        Where(o => (o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00"))
                            && o.DateOfPaid.Value.Year == year
                            && o.DateOfPaid.Value.Month == i
                            && o.CourseEnroll!.CourseId == courseId)
                        .ToList().Sum(o => int.Parse(o.Amount));
                    list.Add(value);
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    int value = _context.StudentFees.
                        Where(o => (o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00"))
                            && o.DateOfPaid.Value.Year == year
                            && o.DateOfPaid.Value.Month == i)
                        .ToList().Sum(o => int.Parse(o.Amount));
                    list.Add(value);
                }
            }
            return list;
        }
    }
}
