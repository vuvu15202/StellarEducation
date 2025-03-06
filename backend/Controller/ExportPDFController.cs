using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPNET_API.Services;
using ASPNET_API.Infrastructure.Data;
using ASPNET_API.Authorization;
using ASPNET_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportPDFController : ControllerBase
    {
        private readonly PDFService _pdfService;

        private readonly DonationWebApp_v2Context _context;

        public ExportPDFController(PDFService pdfService, DonationWebApp_v2Context context)
        {
            _pdfService = pdfService;
            _context = context;
        }

        [HttpGet]
        [Authorize(RoleEnum.Student)]
        [Route("generate/{studentId}/{courseId}")]
        public async Task<IActionResult> GeneratePdf(int studentId, int courseId)
        {
            var compelteCourse = await _context.CourseEnrolls.Include(c => c.User).Include(c => c.Course).Where(c => c.UserId == studentId && c.CourseId == courseId).FirstOrDefaultAsync();
            if (compelteCourse == null)
            {
                return NotFound("Student is not found or you haven't enrolled this course.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Services", "templateExport.html");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }
            var fileContent = await System.IO.File.ReadAllTextAsync(filePath);


            if (fileContent == null || string.IsNullOrWhiteSpace(fileContent))
            {
                return BadRequest("Invalid request.");
            }

            fileContent = fileContent.Replace("((name))", compelteCourse.User.FirstName + " " + compelteCourse.User.LastName)
                .Replace("((course))", compelteCourse.Course.Name)
                .Replace("((date))", DateTime.Now.ToString("MM/dd/yyyy"));
            var pdfBytes = _pdfService.GeneratePdf(fileContent);

            return File(pdfBytes, "application/pdf", "generated.pdf");
        }
    }

    public class PdfRequest
    {
        public string HtmlContent { get; set; }
    }
}
