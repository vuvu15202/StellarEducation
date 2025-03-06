//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Office2013.Word;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol;
//using ASPNET_API.Authorization;
//using ASPNET_API.Models;
//using ASPNET_API.Models.DTO;
//using ASPNET_API.Models.Entity;
//using ASPNET_API.temp;

//namespace ASPNET_API.Controller
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentFeesController : ControllerBase
//    {
//        private readonly DonationWebApp_v2Context _context;
//        private readonly IMapper _mapper;

//        public StudentFeesController(DonationWebApp_v2Context context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
//        [HttpGet("report")]
//        public IActionResult GetReport()
//        {
//            var totalamount = _context.StudentFees.Where(o => o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00")).ToList().Sum(o => int.Parse(o.Amount));
//            var amountofmonth = _context.StudentFees.
//                Where(o => (o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00"))
//                    && o.DateOfPaid.Value.Year == DateTime.Now.Year
//                    && o.DateOfPaid.Value.Month == DateTime.Now.Month)
//                .ToList().Sum(o => int.Parse(o.Amount));


//            return Ok(new ReportStudentFee() { TotalAmount = totalamount, AmountOfMonth = amountofmonth });
//        }






//        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
//        [HttpGet("CoursesStudentFees")]
//        public IActionResult GetAllProjectsAndBiling()
//        {
//            var courses = _context.Courses.ToList();
//            var coursesDTO = _mapper.Map<List<CourseDTO>>(courses);
//            foreach (var item in coursesDTO)
//            {
//                item.TotalStudent = _context.StudentFees.Count();
//                item.TotalStudentFee = _context.StudentFees.Where(s => s.CourseEnroll.CourseId == item.CourseId).ToList().Sum(s => int.Parse(s.Amount));
//                item.StudentFees = _context.StudentFees.Where(s => s.CourseEnroll.CourseId == item.CourseId).ToList();
//            }

//            coursesDTO = coursesDTO.OrderByDescending(s => s.TotalStudentFee).ToList();
//            return Ok(coursesDTO);
//        }

//        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
//        [HttpGet("CourseStudentFees/{Id}")]
//        public IActionResult GetProjectsAndBilingByProjectId(int Id)
//        {
//            var d = _context.Courses.SingleOrDefault(p => p.CourseId == Id);
//            var courseDTO = _mapper.Map<CourseDTO>(d);
//            courseDTO.TotalStudent = _context.StudentFees.Count();
//            courseDTO.TotalStudentFee = _context.StudentFees.Where(s => s.CourseEnroll.CourseId == courseDTO.CourseId).ToList().Sum(s => int.Parse(s.Amount));
//            courseDTO.StudentFees = _context.StudentFees.Where(s => s.CourseEnroll.CourseId == courseDTO.CourseId).ToList();

//            return Ok(courseDTO);
//        }

//        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
//        [HttpGet("handlingbills")]
//        public IActionResult GetAllHandingBill()
//        {
//            var studentfees = _context.StudentFees
//                .Where(o => !o.ErrorCode.Equals("00") && !o.ErrorCode.Equals("0"))
//                .OrderByDescending(o => o.DateOfPaid)
//                .ToList();
//            var studentfeeDTOs = _mapper.Map<List<StudentFeeDTO>>(studentfees);
//            foreach (var studentfeeDTO in studentfeeDTOs)
//            {
//                var ce = _context.CourseEnrolls.SingleOrDefault(c => c.StudentFeeId == studentfeeDTO.StudentFeeId);
//                if (ce != null)
//                {
//                    var course = _context.Courses.SingleOrDefault(c => c.CourseId == ce.CourseId);
//                    studentfeeDTO.Course = _mapper.Map<CourseDTO>(course);
//                }

//            }
//            return Ok(studentfeeDTOs);
//        }

//        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
//        [HttpPost("GetStudentFeesByDate")]
//        public IActionResult GetBillByDate([FromBody] filterDateOfDonation filterDate)
//        {
//            List<StudentFee> studentfees;
//            if (filterDate.fromDate == null && filterDate.toDate == null)
//            {
//                studentfees = _context.StudentFees.OrderByDescending(o => o.DateOfPaid).ToList();

//            }
//            else
//            {
//                studentfees = _context.StudentFees
//                .Where(o => o.DateOfPaid >= filterDate.fromDate && o.DateOfPaid <= filterDate.toDate
//                                    || o.DateOfPaid >= filterDate.fromDate && o.DateOfPaid == null
//                                    || o.DateOfPaid == null && o.DateOfPaid <= filterDate.toDate)
//                .OrderByDescending(o => o.DateOfPaid).ToList();
//            }
//            var studentfeeDTOs = _mapper.Map<List<StudentFeeDTO>>(studentfees);
//            foreach (var studentfeeDTO in studentfeeDTOs)
//            {
//                var ce = _context.CourseEnrolls.SingleOrDefault(c => c.StudentFeeId == studentfeeDTO.StudentFeeId);
//                if (ce != null)
//                {
//                    var course = _context.Courses.SingleOrDefault(c => c.CourseId == ce.CourseId);
//                    studentfeeDTO.Course = _mapper.Map<CourseDTO>(course);
//                }

//            }
//            return Ok(studentfeeDTOs);
//        }


//        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
//        [HttpPost("export")]
//        public IActionResult GetExport([FromBody] filterDateOfDonation filterDate)
//        {
//            List<StudentFee> studentfees;
//            if (filterDate.fromDate == null && filterDate.toDate == null)
//            {
//                studentfees = _context.StudentFees.OrderByDescending(o => o.DateOfPaid).ToList();

//            }
//            else
//            {
//                studentfees = _context.StudentFees
//                .Where(o => o.DateOfPaid >= filterDate.fromDate && o.DateOfPaid <= filterDate.toDate
//                                    || o.DateOfPaid >= filterDate.fromDate && o.DateOfPaid == null
//                                    || o.DateOfPaid == null && o.DateOfPaid <= filterDate.toDate)
//                .OrderByDescending(o => o.DateOfPaid).ToList();
//            }
//            var studentfeeDTOs = _mapper.Map<List<StudentFeeDTO>>(studentfees);
//            foreach (var studentfeeDTO in studentfeeDTOs)
//            {
//                var ce = _context.CourseEnrolls.SingleOrDefault(c => c.StudentFeeId == studentfeeDTO.StudentFeeId);
//                if (ce != null)
//                {
//                    var course = _context.Courses.SingleOrDefault(c => c.CourseId == ce.CourseId);
//                    studentfeeDTO.Course = _mapper.Map<CourseDTO>(course);
//                }

//            }
//            var fileName = "StudentFees.xlsx";
//            return GenerateExcel(fileName, studentfeeDTOs);
//        }

//        private FileResult GenerateExcel(string fileName, IEnumerable<StudentFeeDTO> studentfees)
//        {
//            DataTable dataTable = new DataTable("StudentFees");
//            dataTable.Columns.AddRange(new DataColumn[]
//            {
//                new DataColumn("StudentFeeId"),
//                new DataColumn("PaymentMethod"),
//                new DataColumn("BankCode"),
//                new DataColumn("Amount"),
//                new DataColumn("OrderInfo"),
//                new DataColumn("ErrorCode"),
//                new DataColumn("LocalMessage"),
//                new DataColumn("DateOfPaid")

//            });

//            foreach (var studentfee in studentfees)
//            {
//                dataTable.Rows.Add(studentfee.StudentFeeId,
//                    studentfee.PaymentMethod,
//                    studentfee.BankCode,
//                    studentfee.Amount,
//                    studentfee.OrderInfo,
//                    studentfee.ErrorCode,
//                    studentfee.LocalMessage,
//                    studentfee.DateOfPaid);
//            }

//            using (XLWorkbook wb = new XLWorkbook())
//            {
//                wb.Worksheets.Add(dataTable);
//                using (MemoryStream stream = new MemoryStream())
//                {
//                    wb.SaveAs(stream);

//                    return File(stream.ToArray(),
//                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
//                        fileName);
//                }
//            }

//        }

//    }
//    public class filterDateOfPayment
//    {
//        public DateTime? fromDate { get; set; }
//        public DateTime? toDate { get; set; }

//    }

//    public class ReportStudentFee
//    {
//        public long TotalAmount { get; set; }
//        public long AmountOfMonth { get; set; }
//    }
//    public class filterDateOfDonation
//    {
//        public DateTime? fromDate { get; set; }
//        public DateTime? toDate { get; set; }

//    }
//}
